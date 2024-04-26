using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities;

public interface IEntityCollection<TEntity> : IList<TEntity> {
    event PropertyChangedEventHandler?  PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;
}