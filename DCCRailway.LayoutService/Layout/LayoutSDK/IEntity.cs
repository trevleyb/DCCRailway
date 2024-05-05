namespace DCCRailway.LayoutService.Layout.LayoutSDK;

public interface IEntity<TEntity> {
    Task<TEntity?> Find(Func<TEntity, bool> predicate);
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> GetByNameAsync(string name);
    IAsyncEnumerable<TEntity> GetAllAsync();
    IAsyncEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
}