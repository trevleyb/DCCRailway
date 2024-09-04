using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using DCCRailway.Common.Entities.Base;

namespace DCCRailway.Common.Entities.Collection;

[Serializable]
public abstract class LayoutRepository<TEntity> : ObservableCollection<TEntity>, ILayoutRepository<TEntity> where TEntity : ILayoutEntity {
    private readonly SemaphoreSlim _atomicMutex = new(1, 1);
    private readonly SemaphoreSlim _nextIDMutex = new(1, 1);

    [JsonInclude] public string Prefix { get; set; }

    public TEntity? this[string id] => GetByID(id);

    public bool ContainsID(string id) => GetByID(id) != null;

    public IList<TEntity> GetAll() {
        return Items;
    }

    public IList<TEntity> GetAll(Func<TEntity, bool> predicate) {
        return Items.Select(x => x).Where(predicate).ToList();
    }

    public TEntity? Find(Func<TEntity, bool> predicate) {
        return Items.FirstOrDefault(predicate);
    }

    public TEntity? GetByID(string id) {
        return Find(x => x.Id == id) ?? default(TEntity);
    }

    public TEntity? GetByName(string name) {
        return Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public TEntity? Update(TEntity entity) {
        try {
            _atomicMutex.Wait();
            var index = IndexOf(entity);
            if (index >= 0) {
                Items[index] = entity;
                return entity;
            } else {
                if (string.IsNullOrEmpty(entity.Id)) entity.Id = GetNextID();
                base.Add(entity);
                return this[entity.Id];
            }
        } finally {
            _atomicMutex.Release();
        }
    }

    public new TEntity? Add(TEntity entity) {
        try {
            _atomicMutex.Wait();
            if (string.IsNullOrEmpty(entity.Id)) entity.Id = GetNextID();
            base.Add(entity);
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
            var item = GetByID(id);
            if (item is not null) {
                Remove(item);
                return item;
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
            var ids = new List<string>();
            foreach (var item in Items) ids.Add(item.Id);
            foreach (var id in ids) this.Delete(id);
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
            if (this.Count > 0) {
                var maxId = Items.Where(e => int.TryParse(string.IsNullOrEmpty(Prefix) ? e.Id : e.Id.Replace(Prefix, ""), out _)).Max(e => int.Parse(string.IsNullOrEmpty(Prefix) ? e.Id : e.Id.Replace(Prefix, "")));
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
}