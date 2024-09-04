using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DCCRailway.Common.Entities.Base;

[Serializable]
public abstract partial class LayoutEntity : ObservableObject, ILayoutEntity {
    [ObservableProperty] private string     _description = "";
    [ObservableProperty] private string     _id          = "";
    [ObservableProperty] private string     _name        = "";
    [ObservableProperty] private Parameters _parameters  = [];

    [JsonConstructor]
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

    [JsonIgnore] public bool IsDirty     { get; set; } = false;
    [JsonIgnore] public bool IsTemporary { get; set; } = false;
}