using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Events;

namespace DCCRailway.Common.Entities.Collection;

public delegate void RepositoryChangedEventHandler(object sender, RepositoryChangedEventArgs args);

[Serializable]
public abstract class LayoutRepository<TEntity> : ConcurrentDictionary<string, TEntity>, ILayoutRepository<TEntity> where TEntity : ILayoutEntity {
    private readonly SemaphoreSlim _atomicMutex = new(1, 1);
    private readonly SemaphoreSlim _nextIDMutex = new(1, 1);

    [JsonInclude] public string Prefix { get; set; }

    public event RepositoryChangedEventHandler? RepositoryChanged;

    public IList<TEntity> GetAll() {
        return Values.ToList();
    }

    public IList<TEntity> GetAll(Func<TEntity, bool> predicate) {
        return Values.Select(x => x).Where(predicate).ToList();
    }

    public TEntity? Find(Func<TEntity, bool> predicate) {
        return Values.FirstOrDefault(predicate);
    }

    public TEntity? GetByID(string id) {
        return Contains(id) ? this[id] : default(TEntity?) ?? default(TEntity?);
    }

    public TEntity? GetByName(string name) {
        return Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public TEntity? IndexOf(int index) {
        return this.ElementAtOrDefault(index).Value;
    }

    public TEntity? Update(TEntity entity) {
        try {
            _atomicMutex.Wait();
            if (ContainsKey(entity.Id)) {
                this[entity.Id] = entity;
                OnItemChanged(entity.Id, RepositoryChangeAction.Update);
                return entity;
            } else {
                if (string.IsNullOrEmpty(entity.Id)) entity.Id = GetNextID();
                if (TryAdd(entity.Id, entity)) OnItemChanged(entity.Id, RepositoryChangeAction.Add);
                return this[entity.Id];
            }
        } finally {
            _atomicMutex.Release();
        }
    }

    public TEntity? Add(TEntity entity) {
        try {
            _atomicMutex.Wait();
            if (string.IsNullOrEmpty(entity.Id)) entity.Id = GetNextID();
            if (TryAdd(entity.Id, entity)) OnItemChanged(entity.Id, RepositoryChangeAction.Add);
            return this[entity.Id];
        } catch {
            throw new KeyNotFoundException("Provided key in the Entity does not exist.");
        } finally {
            _atomicMutex.Release();
        }
    }

    public TEntity? Delete(string id) {
        try {
            _atomicMutex.Wait();

            if (ContainsKey(id)) {
                if (TryRemove(id, out var removed)) OnItemChanged(id, RepositoryChangeAction.Delete);
                return removed;
            }

            return default;
        } catch {
            throw new KeyNotFoundException("Provided key in the Entity does not exist.");
        } finally {
            _atomicMutex.Release();
        }
    }

    public void DeleteAll() {
        _atomicMutex.Wait();

        try {
            var keys = new List<string>(Keys);

            foreach (var key in keys) {
                if (TryRemove(key, out var removed)) {
                    OnItemChanged(key, RepositoryChangeAction.Delete);
                }
            }
        } finally {
            _atomicMutex.Release();
        }
    }

    /// <summary>
    ///     Each item in the collection must be UNIQUE and have a Unique ID. Originally this was a GUID but
    ///     that is not user friendly so changed it to be a sequential number with a prefix. This code looks
    ///     at the current collection and finds the next available ID.
    /// </summary>
    public string GetNextID() {
        _nextIDMutex.WaitAsync();

        try {
            var nextID = 1;

            // sort the current collection and find the highest number in the collection and
            // calculate a new ID based on the entities.Prefix and the next sequential number.
            if (this.Any()) {
                var maxId = Keys.Where(e => int.TryParse(string.IsNullOrEmpty(Prefix) ? e : e.Replace(Prefix, ""), out _)).Max(e => int.Parse(string.IsNullOrEmpty(Prefix) ? e : e.Replace(Prefix, "")));
                nextID = maxId + 1;
            }

            return $"{Prefix ?? ""}{nextID:D4}";
        } catch (Exception ex) {
            throw new ApplicationException("Somehow could not create a new unique identifier.", ex);
        } finally {
            _nextIDMutex.Release();
        }
    }

    private void ValidatePath(string pathname) {
        try {
            if (!Directory.Exists(pathname)) Directory.CreateDirectory(pathname);
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to create or access the specified path for the config files '{pathname}'", ex);
        }
    }

    protected bool Contains(string id) {
        return ContainsKey(id);
    }

    protected bool Contains(TEntity item) {
        return ContainsKey(item.Id);
    }

    private void OnItemChanged(string id, RepositoryChangeAction action) {
        RepositoryChanged?.Invoke(this, new RepositoryChangedEventArgs(GetType().Name, id, action));
    }

    private void OnItemChanged(RepositoryChangedEventArgs e) {
        RepositoryChanged?.Invoke(this, e);
    }

    /*
    public IAsyncEnumerable<TEntity> GetAllAsync() {
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
            if (string.IsNullOrEmpty(entity.Id)) entity.Id = GetNextIDAsync();
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
    public          string     GetNextID()            => GetNextIDAsync();
*/
}