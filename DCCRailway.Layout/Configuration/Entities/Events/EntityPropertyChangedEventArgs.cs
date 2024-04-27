using System.ComponentModel;

namespace DCCRailway.Layout.Configuration.Entities.Events;

public class EntityPropertyChangedEventArgs : PropertyChangedEventArgs {

    public IEntity? Entity;
    public readonly object? Value;

    public EntityPropertyChangedEventArgs(string? propertyName) : base(propertyName) { }
    public EntityPropertyChangedEventArgs(IEntity? entity, string? propertyName, object? propertyValue) : base(propertyName) {
        Entity = entity;
        Value = propertyValue;
    }

}