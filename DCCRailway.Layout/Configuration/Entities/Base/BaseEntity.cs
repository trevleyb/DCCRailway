using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Configuration.Entities.Events;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Base;

[Serializable]
public abstract class BaseEntity : PropertyChangeBase, IEntity, INotifyPropertyChanged, INotifyPropertyChanging {
    private Guid       _id              = Guid.NewGuid();
    private string     _name            = "";
    private string     _description     = "";
    private Parameters _parameters      = [];

    public Guid Id               { get => _id;          set => SetField(ref _id, value); }
    public string Name           { get => _name;        set => SetField(ref _name, value); }
    public string Description    { get => _description; set => SetField(ref _description, value); }
    public Parameters Parameters { get => _parameters;  set => SetField(ref _parameters, value); }

    protected BaseEntity() { }
    protected BaseEntity(Guid id) {
        Id = id;
    }
}