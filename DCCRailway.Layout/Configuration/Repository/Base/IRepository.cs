using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Repository.Base;

public interface IRepository<TEntity> {

    event PropertyChangedEventHandler?  PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;

    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity?> Find(Func<TEntity, bool> predicate);
    Task<TEntity?> GetByIDAsync(Guid id);
    Task<TEntity?> GetByNameAsync(string name);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<Task>     AddAsync(TEntity entity);
    Task<Task>     DeleteAsync(Guid id);
    Task<Task>     DeleteAll();
}