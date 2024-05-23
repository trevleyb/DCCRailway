using System.Diagnostics;

namespace DCCRailway.Layout.Configuration;

[Serializable]
[DebuggerDisplay($"Command Station='{{Name}}'")]
public class Controller {
    public string     Name                 { get; set; }
    public Adapters   Adapters             { get; set; } = new();
    public Tasks      Tasks                { get; set; } = [];
    public Parameters Parameters           { get; set; } = [];
    public bool       SendStopOnDisconnect { get; set; }
}