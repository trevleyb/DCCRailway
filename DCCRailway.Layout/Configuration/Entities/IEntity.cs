using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities;

public interface IEntity : INotifyPropertyChanged, INotifyPropertyChanging {
    string Id   { get; set; }
    string Name { get; set; }
}