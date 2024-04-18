using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Repository;

public abstract class Repository<T>(Dictionary<Guid, T> collection) : IRepository<T> where T : BaseEntity {

    protected Dictionary<Guid,T> entities = collection;

    public abstract Task<IEnumerable<T>> GetAllAsync();
    public abstract Task<T?> GetAsync(Guid id);
    public abstract Task<T?> AddAsync(T entity);
    public abstract Task<T?> UpdateAsync(T entity);
    public abstract Task<T?> Find(string name);
    public abstract Task<Task> DeleteAsync(Guid id);

    public Task<T?> this[string name] => Find(x => x.Name.Equals(name,StringComparison.OrdinalIgnoreCase));
    public async Task<T?> Find(Func<T, bool> predicate) {
        return await Task.FromResult(entities.Values.FirstOrDefault(predicate));
    }
}