using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

public interface IEntityCollection<TEntity> : IList<TEntity> {
    event PropertyChangedEventHandler?  PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;
}