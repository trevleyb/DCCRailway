using System.ComponentModel;
using DCCRailway.Layout.Layout.Base;

namespace DCCRailway.Layout.Layout.Events;

public class EntityPropertyChangedEventArgs : PropertyChangedEventArgs {

    public LayoutEntity? Entity;
    public readonly object? Value;

    public EntityPropertyChangedEventArgs(string? propertyName) : base(propertyName) { }
    public EntityPropertyChangedEventArgs(LayoutEntity? entity, string? propertyName, object? propertyValue) : base(propertyName) {
        Entity = entity;
        Value = propertyValue;
    }

}