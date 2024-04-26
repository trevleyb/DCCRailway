using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Base;

public interface IEntity<TKey> : INotifyPropertyChanged, INotifyPropertyChanging {
    TKey Id { get; set; }
    string Name { get; set; }
}