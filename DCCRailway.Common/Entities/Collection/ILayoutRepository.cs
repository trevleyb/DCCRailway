using DCCRailway.Common.Entities.Base;

namespace DCCRailway.Common.Entities.Collection;

public interface ILayoutRepository<TEntity> : IObservableConcurrentDictionary where TEntity : ILayoutEntity {
    IEnumerable<KeyValuePair<string, TEntity>> AsEnumerable { get; }

    IList<TEntity> GetAll();
    IList<TEntity> GetAll(Func<TEntity, bool> predicate);
    TEntity?       Find(Func<TEntity, bool> predicate);
    TEntity?       GetByID(string id);
    TEntity?       GetByName(string name);
    TEntity?       Update(TEntity entity);
    TEntity?       Add(TEntity entity);
    TEntity?       Delete(string id);
    void           DeleteAll();
    string         GetNextID();
}