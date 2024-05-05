using DCCRailway.LayoutService.Layout.Base;

namespace DCCRailway.LayoutService.Layout.Collection;

public interface ILayoutRepository<TEntity> : IDictionary<string, TEntity>
    where TEntity : LayoutEntity {

    event RepositoryChangedEventHandler? RepositoryChanged;

    IEnumerable<TEntity> GetAllAsync();
    IEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity?> Find(Func<TEntity, bool> predicate);
    Task<TEntity?> GetByIDAsync(string id);
    Task<TEntity?> GetByNameAsync(string name);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<TEntity?> AddAsync(TEntity entity);
    Task<TEntity?> IndexOf(int index);
    Task<TEntity?> DeleteAsync(string id);
    Task<Task>     DeleteAll();
}