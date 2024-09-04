namespace DCCRailway.Common.Configuration;

/// <summary>
/// Analyser defines a Packet Analyser that can be used to update state and configuration information.
/// </summary>
[Serializable]
public class Analyser {
    public string?    Name       { get; set; }
    public Adapter    Adapter    { get; set; } = new();
    public Parameters Parameters { get; set; } = [];
}