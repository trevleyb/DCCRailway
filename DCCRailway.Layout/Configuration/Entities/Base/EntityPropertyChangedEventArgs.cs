using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Base;

public class EntityPropertyChangedEventArgs : PropertyChangedEventArgs {
    public object? Value;
    public EntityPropertyChangedEventArgs(string? propertyName) : base(propertyName) { }
    public EntityPropertyChangedEventArgs(string? propertyName, object? propertyValue) : base(propertyName) {
        Value = propertyValue;
    }

}