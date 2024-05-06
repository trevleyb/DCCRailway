namespace DCCRailway.Configuration.Entities;

[Serializable]
public class Adapter {
    public string? AdapterName;
    public Parameters Parameters { get; set; } = [];
}