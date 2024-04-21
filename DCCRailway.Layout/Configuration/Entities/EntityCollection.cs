using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities;

[Serializable]
public class EntityCollection<TKey, TEntity> : List<TEntity>, IEntityCollection<TEntity> where TEntity : IEntity<TKey> where TKey : notnull { }