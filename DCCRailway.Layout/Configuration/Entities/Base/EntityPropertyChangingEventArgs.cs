using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Base;

public class EntityPropertyChangingEventArgs : PropertyChangingEventArgs {
    public object? Value;
    public EntityPropertyChangingEventArgs(string? propertyName) : base(propertyName) { }

    public EntityPropertyChangingEventArgs(string? propertyName, object? propertyValue) : base(propertyName) {
        Value = propertyValue;
    }
}