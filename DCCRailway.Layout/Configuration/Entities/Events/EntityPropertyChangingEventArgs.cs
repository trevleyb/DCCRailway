using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Events;

public class EntityPropertyChangingEventArgs : PropertyChangingEventArgs {

    public IEntity? Entity;
    public object?  Value;

    public EntityPropertyChangingEventArgs(string? propertyName) : base(propertyName) { }

    public EntityPropertyChangingEventArgs(IEntity? entity, string? propertyName, object? propertyValue) : base(propertyName) {
        Entity = entity;
        Value  = propertyValue;
    }
}