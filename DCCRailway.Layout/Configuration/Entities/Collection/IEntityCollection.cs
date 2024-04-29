using System.Collections.Specialized;
using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

public interface IEntityCollection<TEntity> : IList<TEntity> {
    public Task<string> GetNextID();

    event NotifyCollectionChangedEventHandler? CollectionChanged;
    event PropertyChangedEventHandler?  PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;
}