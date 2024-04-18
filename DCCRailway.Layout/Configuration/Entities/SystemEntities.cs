using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities;

[Serializable]
public class SystemEntities {
    public Controllers  Controllers { get; set; } = [];
    public Parameters   Parameters { get; set; } = [];

    [JsonIgnore]
    public Manufacturers Manufacturers { get; set; } = [];
}