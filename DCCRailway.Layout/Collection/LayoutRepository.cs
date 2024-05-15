using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Base;
using DCCRailway.Layout.Events;

namespace DCCRailway.Layout.Collection;

public delegate void RepositoryChangedEventHandler(object sender, RepositoryChangedEventArgs args);

[Serializable]
public abstract class LayoutRepository<TEntity>
    : EntityStorage<TEntity>
      , ILayoutRepository<TEntity>
      , IEnumerable<KeyValuePair<string, TEntity>>
    where TEntity : LayoutEntity {
    private readonly SemaphoreSlim _atomicMutex = new(1, 1);
    private readonly SemaphoreSlim _nextIDMutex = new(1, 1);

    protected LayoutRepository(string prefix, string name, string? pathname = null) {
        Prefix   = prefix;
        Name     = name;
        PathName = pathname ?? "./";
    }

    public string Name     { get; set; }
    public string Prefix   { get; init; }
    public string FileName => $"{Name}.{GetType().Name}.json";
    public string FullName => Path.Combine(PathName ?? "", FileName);

    public event RepositoryChangedEventHandler? RepositoryChanged;
    public string                               PathName { get; set; }

    public void Save(string pathname) {
        PathName = pathname;
        ValidatePath(pathname);
        SaveFile(FullName);
    }

    public void Save() {
        ValidatePath(PathName);
        SaveFile(FullName);
    }

    public virtual void Load() => LoadFile(FullName);

    public async IAsyncEnumerable<TEntity> GetAllAsync() {
        await foreach (var item in Values.ConvertToAsyncEnumerable()) {
            yield return item;
        }
    }

    public async IAsyncEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate) {
        await foreach (var item in Values.Select(x => x).Where(predicate).ConvertToAsyncEnumerable()) {
            yield return item;
        }
    }

    public async Task<TEntity?> FindAsync(Func<TEntity, bool> predicate) => await Task.FromResult(Values.FirstOrDefault(predicate));
    public async Task<TEntity?> GetByIDAsync(string id)                  => await Task.FromResult(this[id]);
    public async Task<TEntity?> GetByNameAsync(string name)              => await FindAsync(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    public async Task<TEntity?> IndexOfAsync(int index)                  => await Task.FromResult(this.ElementAtOrDefault(index).Value);

    public async Task<TEntity?> UpdateAsync(TEntity entity) {
        try {
            await _atomicMutex.WaitAsync();
            if (ContainsKey(entity.Id)) {
                this[entity.Id] = entity;
                OnItemChanged(entity.Id, RepositoryChangeAction.Update);
                return await Task.FromResult(entity);
            } else {
                return await Task.FromException<TEntity?>(new KeyNotFoundException("Provided key in the Entity does not exist."));
            }
        } finally {
            _atomicMutex.Release();
        }
    }

    public async Task<TEntity?> AddAsync(TEntity entity) {
        try {
            await _atomicMutex.WaitAsync();
            if (string.IsNullOrEmpty(entity.Id)) entity.Id = await GetNextIDAsync();
            if (TryAdd(entity.Id, entity)) OnItemChanged(entity.Id, RepositoryChangeAction.Add);
            return await Task.FromResult(this[entity.Id]);
        } catch {
            return await Task.FromException<TEntity?>(new KeyNotFoundException("Provided key in the Entity does not exist."));
        } finally {
            _atomicMutex.Release();
        }
    }

    public async Task<TEntity?> DeleteAsync(string id) {
        try {
            await _atomicMutex.WaitAsync();
            if (ContainsKey(id)) {
                if (TryRemove(id, out var removed)) OnItemChanged(id, RepositoryChangeAction.Delete);
                return await Task.FromResult(removed);
            }
            return await Task.FromResult<TEntity?>(null);
        } catch {
            return await Task.FromException<TEntity?>(new KeyNotFoundException("Provided key in the Entity does not exist."));
        } finally {
            _atomicMutex.Release();
        }
    }

    public async Task<Task> DeleteAllAsync() {
        await _atomicMutex.WaitAsync();
        try {
            var keys = new List<string>(Keys);
            foreach (var key in keys) {
                if (TryRemove(key, out var removed)) OnItemChanged(key, RepositoryChangeAction.Delete);
            }
        } finally {
            _atomicMutex.Release();
        }
        return Task.CompletedTask;
    }

    public IList<TEntity> GetAll()                              => GetAllAsync().GetListFromAsyncEnumerable().GetAwaiter().GetResult();
    public IList<TEntity> GetAll(Func<TEntity, bool> predicate) => GetAllAsync(predicate).GetListFromAsyncEnumerable().GetAwaiter().GetResult();
    public TEntity?       Find(Func<TEntity, bool> predicate)   => FindAsync(predicate).GetAwaiter().GetResult();
    public TEntity?       GetByID(string id)                    => GetByIDAsync(id).GetAwaiter().GetResult();
    public TEntity?       GetByName(string name)                => GetByNameAsync(name).GetAwaiter().GetResult();
    public TEntity?       IndexOf(int index)                    => IndexOfAsync(index).GetAwaiter().GetResult();
    public TEntity?       Update(TEntity entity)                => UpdateAsync(entity).GetAwaiter().GetResult();
    public TEntity?       Add(TEntity entity)                   => AddAsync(entity).GetAwaiter().GetResult();
    public TEntity?       Delete(string id)                     => DeleteAsync(id).GetAwaiter().GetResult();
    public void           DeleteAll()                           => DeleteAllAsync().GetAwaiter();

    private void ValidatePath(string pathname) {
        try {
            if (!Directory.Exists(pathname)) Directory.CreateDirectory(pathname);
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to create or access the specified path for the config files '{pathname}'", ex);
        }
    }

    protected async Task<bool> Contains(string id)    => await Task.FromResult(ContainsKey(id));
    protected async Task<bool> Contains(TEntity item) => await Task.FromResult(ContainsKey(item.Id));
    public          string     GetNextID()            => GetNextIDAsync().GetAwaiter().GetResult();

    /// <summary>
    ///     Each item in the collection must be UNIQUE and have a Unique ID. Originally this was a GUID but
    ///     that is not user friendly so changed it to be a sequential number with a prefix. This code looks
    ///     at the current collection and finds the next available ID.
    /// </summary>
    public async Task<string> GetNextIDAsync() {
        await _nextIDMutex.WaitAsync();
        try {
            var nextID = 1;

            // sort the current collection and find the highest number in the collection and
            // calculate a new ID based on the entities.Prefix and the next sequential number.
            if (this.Any()) {
                var maxId = Keys
                           .Where(e => int.TryParse(e.Replace(Prefix, ""), out _))
                           .Max(e => int.Parse(e.Replace(Prefix, "")));
                nextID = maxId + 1;
            }
            return $"{Prefix}{nextID:D4}";
        } catch (Exception ex) {
            Logger.Log.Error("Unable to determine the next sequence: {0}", ex.Message);
            throw;
        } finally {
            _nextIDMutex.Release();
        }
    }

    private void OnItemChanged(string id, RepositoryChangeAction action) {
        RepositoryChanged?.Invoke(this, new RepositoryChangedEventArgs(GetType().Name, id, action));
    }

    private void OnItemChanged(RepositoryChangedEventArgs e) {
        RepositoryChanged?.Invoke(this, e);
    }
}