using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Collection;

public interface ILayoutRepository<TEntity> : IDictionary<string, TEntity>
    where TEntity : LayoutEntity {
    event RepositoryChangedEventHandler? RepositoryChanged;

    IAsyncEnumerable<TEntity> GetAllAsync();
    IAsyncEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity?>            FindAsync(Func<TEntity, bool> predicate);
    Task<TEntity?>            GetByIDAsync(string id);
    Task<TEntity?>            GetByNameAsync(string name);
    Task<TEntity?>            UpdateAsync(TEntity entity);
    Task<TEntity?>            AddAsync(TEntity entity);
    Task<TEntity?>            IndexOfAsync(int index);
    Task<TEntity?>            DeleteAsync(string id);
    Task<Task>                DeleteAllAsync();

    IList<TEntity> GetAll();
    IList<TEntity> GetAll(Func<TEntity, bool> predicate);
    TEntity?       Find(Func<TEntity, bool> predicate);
    TEntity?       GetByID(string id);
    TEntity?       GetByName(string name);
    TEntity?       Update(TEntity entity);
    TEntity?       Add(TEntity entity);
    TEntity?       IndexOf(int index);
    TEntity?       Delete(string id);
    void           DeleteAll();
}