using System.ComponentModel;
using DCCRailway.Common.Entities.Base;

namespace DCCRailway.Common.Events;

public class EntityPropertyChangedEventArgs : PropertyChangedEventArgs {
    public readonly object?       Value;
    public          LayoutEntity? Entity;

    public EntityPropertyChangedEventArgs(string? propertyName) : base(propertyName) { }

    public EntityPropertyChangedEventArgs(LayoutEntity? entity, string? propertyName, object? propertyValue) : base(propertyName) {
        Entity = entity;
        Value  = propertyValue;
    }
}