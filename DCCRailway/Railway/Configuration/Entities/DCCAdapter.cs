namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
public class DCCAdapter {
    public string? Name;
    public Parameters Parameters { get; set; } = [];
}