using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities.Collection;

public interface ILayoutRepository<TEntity> : IDictionary<string, TEntity> where TEntity : ILayoutEntity {
    event RepositoryChangedEventHandler? RepositoryChanged;

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
    string         GetNextID();
}