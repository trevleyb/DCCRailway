using System.ComponentModel;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Repository.Base;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity  {

    protected readonly IEntityCollection<TEntity> entities;
    public event PropertyChangedEventHandler?  PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected Repository(IEntityCollection<TEntity> collection) {
        entities = collection;
        entities.PropertyChanged += OnPropertyChanged;
        entities.PropertyChanging += OnPropertyChanging;
    }

    protected void OnPropertyChanged(object? sender, PropertyChangedEventArgs propertyChangedEventArgs) {
        PropertyChanged?.Invoke(this, propertyChangedEventArgs);
    }
    protected void OnPropertyChanging(object? sender, PropertyChangingEventArgs propertyChangingEventArgs) {
        PropertyChanging?.Invoke(this, propertyChangingEventArgs);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() {
        try {
            return await Task.FromResult(entities.ToList());
        } catch (Exception ex) {
            return await Task.FromException<IEnumerable<TEntity>>(ex);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity,bool> predicate) {
        try {
            return await Task.FromResult(entities.Select(x => x).Where(predicate).ToList());
        } catch (Exception ex) {
            return await Task.FromException<IEnumerable<TEntity>>(ex);
        }
    }

    public async Task<TEntity?> GetByIDAsync(string id) {
        try {
            return await Task.FromResult(entities.FirstOrDefault(x => x.Id.Equals(id)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public async Task<TEntity?> GetByNameAsync(string name) {
        try {
            return await Task.FromResult(entities.FirstOrDefault(x => x.Name.Equals(name,StringComparison.InvariantCultureIgnoreCase)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public async Task<Task> AddAsync(TEntity entity) {
        try {
            if (string.IsNullOrEmpty(entity.Id)) entity.Id = await GetNextID();
            var index = FindIndexOf(entity.Id).Result;
            if (index == -1) entities.Add(entity);
            return Task.CompletedTask;
        } catch (Exception ex) {
            return Task.FromException(ex);
        }
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity) {
        try {
            var index = await FindIndexOf(entity.Id);
            if (index != -1) entities.RemoveAt(index);
            await AddAsync(entity);
            return await Task.FromResult(entity);
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public async Task<Task> DeleteAsync(string id) {
        try {
            var index = await FindIndexOf(id);
            if (index != -1) entities.RemoveAt(index);
            return Task.CompletedTask;
        } catch (Exception ex) {
            return Task.FromException(ex);
        }
    }

    public async Task<Task> DeleteAll() {
        entities.Clear();
        return await Task.FromResult(Task.CompletedTask);
    }

    public async Task<TEntity?> Find(string name) {
        try {
            return await Task.FromResult(entities.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    private Task<int> FindIndexOf(string id) {
        foreach (var item in entities) {
            if (item.Id.Equals(id)) {
                return Task.FromResult(entities.IndexOf(item));
            }
        }
        return Task.FromResult(-1);
    }

    public Task<TEntity?> this[string name] => Find(x => x.Name.Equals(name,StringComparison.OrdinalIgnoreCase));
    public async Task<TEntity?> Find(Func<TEntity, bool> predicate) {
        return await Task.FromResult(entities.FirstOrDefault<TEntity>(predicate));
    }

    public async Task<string> GetNextID() {
        return await Task.FromResult(await entities.GetNextID());
    }

}