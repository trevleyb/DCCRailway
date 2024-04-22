using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public abstract class BaseRepository<TKey,TEntity>(IEntityCollection<TEntity> collection) : Repository<TKey,TEntity>(collection) where TEntity : IEntity<TKey> where TKey : notnull {

    public override async Task<IEnumerable<TEntity>> GetAllAsync() {
        try {
            return await Task.FromResult(entities.ToList());
        } catch (Exception ex) {
            return await Task.FromException<IEnumerable<TEntity>>(ex);
        }
    }

    public override async Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity,bool> predicate) {
        try {
            return await Task.FromResult(entities.Select(x => x).Where(predicate).ToList());
        } catch (Exception ex) {
            return await Task.FromException<IEnumerable<TEntity>>(ex);
        }
    }

    public override async Task<TEntity?> GetByIDAsync(TKey id) {
        try {
            return await Task.FromResult(entities.FirstOrDefault(x => x.Id.Equals(id)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public override async Task<Task> AddAsync(TEntity entity) {
        try {
            var index = FindIndexOf(entity.Id).Result;
            if (index == -1) entities.Add(entity);
            return Task.CompletedTask;
        }
        catch (Exception ex) {
            return Task.FromException(ex);
        }
    }

    public override async Task<TEntity?> UpdateAsync(TEntity entity) {
        try {
            var index = await FindIndexOf(entity.Id);
            if (index != -1) entities.RemoveAt(index);
            await AddAsync(entity);
            return await Task.FromResult(entity);
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    public override async Task<Task> DeleteAsync(TKey id) {
        try {
            var index = FindIndexOf(id).Result;
            if (index != -1) entities.RemoveAt(index);
            return Task.CompletedTask;
        } catch (Exception ex) {
            return Task.FromException(ex);
        }
    }

    public override async Task<Task> DeleteAll() {
        entities.Clear();
        return await Task.FromResult(Task.CompletedTask);
    }

    public override async Task<TEntity?> Find(string name) {
        try {
            return await Task.FromResult(entities.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? default(TEntity));
        } catch (Exception ex) {
            return await Task.FromException<TEntity?>(ex);
        }
    }

    /// <summary>
    /// We should rewrite this. There must be an easier way that searching for the key in the collection.
    /// It should not matter as the collections will not get huge, and will be in memory, but this is
    /// not a great way to find this.
    /// </summary>
    /// <param name="id">the identifier to find in the collection</param>
    /// <returns></returns>
    private Task<int> FindIndexOf(TKey id) {
        foreach (var item in entities) {
            if (item.Id.Equals(id)) {
                return Task.FromResult(entities.IndexOf(item));
            }
        }
        return Task.FromResult(-1);
    }
}