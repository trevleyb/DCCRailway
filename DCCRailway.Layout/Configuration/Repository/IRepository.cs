namespace DCCRailway.Layout.Configuration.Repository;

public interface IRepository<TKey,TEntity> {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity?> Find(Func<TEntity, bool> predicate);
    Task<TEntity?> GetByIDAsync(TKey id);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<Task>     AddAsync(TEntity entity);
    Task<Task>     DeleteAsync(TKey id);
    Task<Task>     DeleteAll();
}