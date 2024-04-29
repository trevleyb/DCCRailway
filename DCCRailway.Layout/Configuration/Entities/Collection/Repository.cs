using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

[Serializable]
public class Repository<TEntity> : ObservableCollection<TEntity>, IRepository<TEntity> where TEntity : IEntity {

    private static readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
    private string Prefix { get; init; }

    public new event PropertyChangedEventHandler?  PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    public override event NotifyCollectionChangedEventHandler? CollectionChanged {
        add => base.CollectionChanged += value;
        remove => base.CollectionChanged -= value;
    }

    public Repository(string prefix) {
        Prefix = prefix;
        CollectionChanged += (sender, e) => {
            if (e.NewItems != null) {
                foreach (TEntity newItem in e.NewItems) {
                    newItem.PropertyChanged  += OnPropertyChanged;
                    newItem.PropertyChanging += OnPropertyChanging;
                }
            }

            if (e.OldItems != null) {
                foreach (TEntity oldItem in e.OldItems) {
                    oldItem.PropertyChanged  -= OnPropertyChanged;
                    oldItem.PropertyChanging -= OnPropertyChanging;
                }
            }
        };
    }

    private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e) {
        PropertyChanging?.Invoke(this, e);
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        PropertyChanged?.Invoke(this, e);
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
                           .Where(e => int.TryParse(e.Id.Replace(Prefix, ""), out _))
                           .Max(e => int.Parse(e.Id.Replace(Prefix, "")));
                nextID = maxId + 1;
            }
            return $"{Prefix}{nextID:D3}";
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to determine the next sequence: {0}", ex.Message);
            throw;
        }
        finally {
            _mutex.Release();
        }
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) {
        //return this.Select(x => Task.FromResult(x)).ToAsyncEnumerable<TEntity>().GetAsyncEnumerator(cancellationToken);
        return this.ToAsyncEnumerable<TEntity>().GetAsyncEnumerator(cancellationToken);
    }

    public async Task<TEntity?> Find(string name) {
        try {
            return await Task.FromResult(this.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public Task<TEntity?> this[string id] => Find(x => x.Id.Equals(id,StringComparison.OrdinalIgnoreCase));

    public async Task<TEntity?> Find(Func<TEntity, bool> predicate) {
        return await Task.FromResult(this.FirstOrDefault<TEntity>(predicate));
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() {
        try {
            return await Task.FromResult(this.ToList());
        } catch (Exception ex) {
            return await Task.FromException<IEnumerable<TEntity>>(ex);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity,bool> predicate) {
        try {
            return await Task.FromResult(this.Select(x => x).Where(predicate).ToList());
        } catch (Exception ex) {
            return await Task.FromException<IEnumerable<TEntity>>(ex);
        }
    }

    public async Task<TEntity?> GetByIDAsync(string id) {
        try {
            return await Task.FromResult(this.FirstOrDefault(x => x.Id.Equals(id)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public async Task<TEntity?> GetByNameAsync(string name) {
        try {
            return await Task.FromResult(this.FirstOrDefault(x => x.Name.Equals(name,StringComparison.InvariantCultureIgnoreCase)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public new void Add(TEntity entity) {
        try {
            if (string.IsNullOrEmpty(entity.Id)) entity.Id = GetNextID().Result;
            var index = FindIndexOf(entity.Id).Result;
            if (index == -1) this.Add(entity);
            return;
        } catch (Exception ex) {
            throw new ApplicationException($"Could not add {entity.GetType()} to Collection: {ex.Message}",ex);
        }
    }

    public async Task<Task> AddAsync(TEntity entity) {
        try {
            if (string.IsNullOrEmpty(entity.Id)) entity.Id = await GetNextID();
            var index = FindIndexOf(entity.Id).Result;
            if (index == -1) this.Add(entity);
            return Task.CompletedTask;
        } catch (Exception ex) {
            return Task.FromException(ex);
        }
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity) {
        try {
            var index = await FindIndexOf(entity.Id);
            if (index != -1) this.RemoveAt(index);
            await AddAsync(entity);
            return await Task.FromResult(entity);
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public async Task<Task> DeleteAsync(string id) {
        try {
            var index = await FindIndexOf(id);
            if (index != -1) this.RemoveAt(index);
            return Task.CompletedTask;
        } catch (Exception ex) {
            return Task.FromException(ex);
        }
    }

    public async Task<Task> DeleteAll() {
        this.Clear();
        return await Task.FromResult(Task.CompletedTask);
    }
}