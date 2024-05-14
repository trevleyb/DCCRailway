namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
public class Adapter {
    public string? Name;
    public Parameters Parameters { get; set; } = [];
}