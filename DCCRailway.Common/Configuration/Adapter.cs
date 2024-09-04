namespace DCCRailway.Common.Configuration;

[Serializable]
public class Adapter {
    public string?    Name       { get; set; }
    public Parameters Parameters { get; set; } = [];
}