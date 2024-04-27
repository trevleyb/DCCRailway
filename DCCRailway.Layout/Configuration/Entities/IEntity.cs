using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities;

public interface IEntity : INotifyPropertyChanged, INotifyPropertyChanging {
    Guid Id { get; set; }
    string Name { get; set; }
}