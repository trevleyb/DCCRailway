using System.Collections.Specialized;
using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

public interface IRepository<TEntity> : IList<TEntity> {

    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity?> Find(Func<TEntity, bool> predicate);
    Task<TEntity?> GetByIDAsync(string id);
    Task<TEntity?> GetByNameAsync(string name);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<Task>     AddAsync(TEntity entity);
    Task<Task>     DeleteAsync(string id);
    Task<Task>     DeleteAll();
    Task<string>   GetNextID();

    event NotifyCollectionChangedEventHandler? CollectionChanged;
    event PropertyChangedEventHandler?  PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;
}