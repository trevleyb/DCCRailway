namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
public class Adapter {
    public string? Name { get; set; }
    public Parameters Parameters { get; set; } = [];
}