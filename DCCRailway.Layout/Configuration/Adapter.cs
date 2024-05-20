namespace DCCRailway.Layout.Configuration;

[Serializable]
public class Adapter {
    public string?    Name       { get; set; }
    public Parameters Parameters { get; set; } = [];
}