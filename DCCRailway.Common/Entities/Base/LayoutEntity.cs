using System.ComponentModel;
using System.Text.Json.Serialization;
using DCCRailway.Common.Events;

namespace DCCRailway.Common.Entities.Base;

[Serializable]
public abstract class LayoutEntity : PropertyChangeBase, INotifyPropertyChanged, ILayoutEntity {
    private string     _description = "";
    private string     _id          = "";
    private string     _name        = "";
    private Parameters _parameters  = [];

    protected LayoutEntity() { }

    protected LayoutEntity(string id) {
        // This is correct, but the ID is only needed once it is added to a collection/repository
        // As part of that Add function, if there is no ID, then one is generated automatically.
        // if (string.IsNullOrEmpty(id)) throw new ArgumentException("All entities must have a unique ID");
        Id = id;
    }

    protected LayoutEntity(string id, string name, string description = "") {
        Id          = id;
        Name        = name;
        Description = description;
    }

    public string Id {
        get => _id;
        set => SetField(ref _id, value);
    }

    public string Name {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Description {
        get => _description;
        set => SetField(ref _description, value);
    }

    public Parameters Parameters {
        get => _parameters;
        set => SetField(ref _parameters, value);
    }

    [JsonIgnore] public bool IsDirty     { get; set; } = false;
    [JsonIgnore] public bool IsTemporary { get; set; } = false;
}