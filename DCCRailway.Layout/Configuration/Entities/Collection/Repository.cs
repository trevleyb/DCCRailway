using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Configuration.Entities.Events;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

[Serializable]
public class Repository<TEntity>(string prefix = "***") : Dictionary<string, TEntity>, IRepository<TEntity>
    where TEntity : IEntity {

    public event RepositoryChangedEventHandler? RepositoryChanged;
    private static readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
    public string Prefix { get; init; } = prefix;

    public TEntity Add(TEntity item) {
        if (string.IsNullOrEmpty(item.Id)) item.Id = GetNextID().Result;
        if (TryAdd(item.Id, item)) {
            OnItemChanged(item.Id, RepositoryChangeAction.Add);
            return item;
        }
        return this[item.Id];
    }

    public bool Contains(string id) => this.ContainsKey(id);
    public bool Contains(TEntity item) => this.ContainsKey(item.Id);

    public IAsyncEnumerable<TEntity> GetAllAsync() => Values.ToAsyncEnumerable();
    public IAsyncEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate) => Values.Select( x=> x).Where(predicate).ToAsyncEnumerable();
    public async Task<TEntity?> Find(Func<TEntity, bool> predicate) => await Task.FromResult(Values.FirstOrDefault(predicate));
    public async Task<TEntity?> GetByIDAsync(string id) => await Task.FromResult(this[id]);
    public async Task<TEntity?> GetByNameAsync(string name) => await Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    public async Task<TEntity?> IndexOf(int index) => await Task.FromResult(this.ElementAtOrDefault(index).Value);
    public async Task<TEntity?> UpdateAsync(TEntity entity) {
        if (ContainsKey(entity.Id)) {
            this[entity.Id] = entity;
            OnItemChanged(entity.Id, RepositoryChangeAction.Update);
            return await Task.FromResult(entity);
        }
        else return await Task.FromException<TEntity?>(new KeyNotFoundException("Provided key in the Entity does not exist."));
    }

    public async Task<TEntity?> AddAsync(TEntity entity) {
        Add(entity);
        return await Task.FromResult(entity);
    }

    public async Task<Task> DeleteAsync(string id) {
        try {
            if (ContainsKey(id)) {
                Remove(id);
                OnItemChanged(id, RepositoryChangeAction.Delete);
                return await Task.FromResult(Task.CompletedTask);
            } else {
                return Task.FromException(new KeyNotFoundException("The provided ID does not exist in the repository."));
            }
        } catch (Exception ex) {
            return Task.FromException(ex);
        }
    }

    public async Task<Task> DeleteAll() {
        await _mutex.WaitAsync();
        try {
            var keys = new List<string>(Keys);
            foreach (var key in keys) {
                OnItemChanged(key, RepositoryChangeAction.Delete);
                Remove(key);
            }
        }
        finally {
            _mutex.Release();
        }
        return await Task.FromResult(Task.CompletedTask);
    }

    /// <summary>
    /// Each item in the collection must be UNIQUE and have a Unique ID. Originally this was a GUID but
    /// that is not user friendly so changed it to be a sequential number with a prefix. This code looks
    /// at the current collection and finds the next available ID.
    /// </summary>
    public async Task<string> GetNextID() {
        await _mutex.WaitAsync();
        try {
            var nextID = 1;

            // sort the current collection and find the highest number in the collection and
            // calculate a new ID based on the entities.Prefix and the next sequential number.
            if (this.Any()) {
                var maxId = this
                           .Keys
                           .Where(e => int.TryParse(e.Replace(Prefix, ""), out _))
                           .Max(e => int.Parse(e.Replace(Prefix, "")));
                nextID = maxId + 1;
            }
            return $"{Prefix}{nextID:D4}";
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to determine the next sequence: {0}", ex.Message);
            throw;
        } finally {
            _mutex.Release();
        }
    }

    private void OnItemChanged(string id, RepositoryChangeAction action) {
        RepositoryChanged?.Invoke(this, new RepositoryChangedEventArgs(this.GetType().Name, id, action));
    }

    private void OnItemChanged(RepositoryChangedEventArgs e) {
        RepositoryChanged?.Invoke(this, e);
    }

}

public delegate void RepositoryChangedEventHandler(object sender, RepositoryChangedEventArgs args);