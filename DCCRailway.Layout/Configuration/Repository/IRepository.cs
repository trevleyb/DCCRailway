namespace DCCRailway.Layout.Configuration.Repository;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Guid id);
    Task<T?> AddAsync(T entity);
    Task<T?> UpdateAsync(T entity);
    Task<Task> DeleteAsync(Guid id);

    Task<T?> Find(Func<T, bool> predicate);
}