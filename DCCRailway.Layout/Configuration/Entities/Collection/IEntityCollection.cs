using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

public interface IEntityCollection<TEntity> : IList<TEntity> {
    public string Prefix { get; }
    public string NextID { get; }
    event PropertyChangedEventHandler?  PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;
}