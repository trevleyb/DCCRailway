using System.Collections.Specialized;
using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

public interface IRepository<TEntity> : IDictionary<string,TEntity> {

    event RepositoryChangedEventHandler? RepositoryChanged;

    IAsyncEnumerable<TEntity> GetAllAsync();
    IAsyncEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate);
    Task<TEntity?> Find(Func<TEntity, bool> predicate);
    Task<TEntity?> GetByIDAsync(string id);
    Task<TEntity?> GetByNameAsync(string name);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<TEntity?> AddAsync(TEntity entity);
    Task<TEntity?> IndexOf(int index);
    Task<Task>     DeleteAsync(string id);
    Task<Task>     DeleteAll();
    Task<string>   GetNextID();


}