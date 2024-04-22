using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Repository;

public abstract class Repository<TKey,TEntity>(IEntityCollection<TEntity> collection) : IRepository<TKey,TEntity> where TEntity : IEntity<TKey> where TKey : notnull {

    protected readonly IEntityCollection<TEntity> entities = collection;

    public abstract Task<IEnumerable<TEntity>> GetAllAsync();
    public abstract Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity, bool> predicate);
    public abstract Task<TEntity?> GetByIDAsync(TKey id);
    public abstract Task<TEntity?> UpdateAsync(TEntity entity);
    public abstract Task<TEntity?> Find(string name);
    public abstract Task<Task> AddAsync(TEntity entity);
    public abstract Task<Task> DeleteAsync(TKey id);
    public abstract Task<Task> DeleteAll();

    public Task<TEntity?> this[string name] => Find(x => x.Name.Equals(name,StringComparison.OrdinalIgnoreCase));
    public async Task<TEntity?> Find(Func<TEntity, bool> predicate) {
        return await Task.FromResult(entities.FirstOrDefault<TEntity>(predicate));
    }
}