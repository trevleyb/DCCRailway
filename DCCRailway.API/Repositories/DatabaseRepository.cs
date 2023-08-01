using DccRailway.API.Entities;

namespace DccRailway.API.Repositories;

public class DatabaseRepository<TEntity, TID> : IRepository<TEntity, TID> where TEntity : IEntity<TID> where TID : IComparable {
    public TEntity GetById(TID id) {
        throw new NotImplementedException();
    }

    public IEnumerable<TEntity> GetAll() {
        throw new NotImplementedException();
    }

    public TID Add(TEntity obj) {
        throw new NotImplementedException();
    }

    public void Update(TEntity obj) {
        throw new NotImplementedException();
    }

    public void Delete(TEntity obj) {
        throw new NotImplementedException();
    }

    public void Delete(TID id) {
        throw new NotImplementedException();
    }

    public void Save() {
        throw new NotImplementedException();
    }
}