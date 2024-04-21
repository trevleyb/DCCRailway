namespace DCCRailway.Layout.Configuration.Repository;

public interface IRepository<TKey,TEntity> {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(TKey id);
    Task<TEntity?> AddAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<Task> DeleteAsync(TKey id);
    Task<TEntity?> Find(Func<TEntity, bool> predicate);
}