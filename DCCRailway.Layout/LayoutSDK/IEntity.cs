namespace DCCRailway.Layout.LayoutSDK;

public interface IEntity<TEntity> {
    Task<TEntity?>            FindAsync(Func<TEntity, bool> predicate);
    Task<TEntity?>            GetByIdAsync(string id);
    Task<TEntity?>            GetByNameAsync(string name);
    IAsyncEnumerable<TEntity> GetAllAsync();
    IAsyncEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity>             AddAsync(TEntity entity);
    Task<TEntity>             UpdateAsync(TEntity entity);
    Task                      DeleteAsync(string id);

    TEntity?             Find(Func<TEntity, bool> predicate);
    TEntity?             GetById(string id);
    TEntity?             GetByName(string name);
    TEntity              Add(TEntity entity);
    TEntity              Update(TEntity entity);
    void                 Delete(string id);
    IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);
    IEnumerable<TEntity> GetAll();
}