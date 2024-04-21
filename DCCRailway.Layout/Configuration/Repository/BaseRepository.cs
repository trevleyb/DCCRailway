using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public abstract class BaseRepository<TKey,TEntity>(IEntityCollection<TEntity> collection) : Repository<TKey,TEntity>(collection) where TEntity : IEntity<TKey> where TKey : notnull {

    public override async Task<IEnumerable<TEntity>> GetAllAsync() {
        return await Task.FromResult(entities.ToList());
    }

    public override async Task<TEntity?> GetAsync(TKey id) {
        return await Task.FromResult(entities.FirstOrDefault(x =>  x.Id.Equals(id)) ?? default(TEntity));
    }

    public override async Task<TEntity?> AddAsync(TEntity entity) {
        int index = await FindIndexOf(entity.Id);
        if (index == -1) entities.Add(entity);
        return await Task.FromResult(entity);
    }

    public override async Task<TEntity?> UpdateAsync(TEntity entity) {
        int index = await FindIndexOf(entity.Id);
        if (index != -1) entities.RemoveAt(index);
        await AddAsync(entity);
        return await Task.FromResult(entity);
    }

    public override async Task<Task> DeleteAsync(TKey id) {
        int index = await FindIndexOf(id);
        if (index != -1) entities.RemoveAt(index);
        return await Task.FromResult(Task.CompletedTask);
    }

    public override async Task<TEntity?> Find(string name) {
        return await Task.FromResult(entities.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? default(TEntity));
    }

    /// <summary>
    /// We should rewrite this. There must be an easier way that searching for the key in the collection.
    /// It should not matter as the collections will not get huge, and will be in memory, but this is
    /// not a great way to find this.
    /// </summary>
    /// <param name="id">the identifier to find in the collection</param>
    /// <returns></returns>
    private async Task<int> FindIndexOf(TKey id) {
        foreach (var item in entities) {
            if (item.Id.Equals(id)) {
                return entities.IndexOf(item);
            }
        }
        return -1;
    }
}