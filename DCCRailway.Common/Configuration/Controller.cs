using System.Diagnostics;

namespace DCCRailway.Common.Configuration;

[Serializable]
[DebuggerDisplay($"Command Station='{{Name}}'")]
public class Controller {
    public string     Name                 { get; set; }
    public Adapter    Adapter              { get; set; } = new();
    public Tasks      Tasks                { get; set; } = [];
    public Parameters Parameters           { get; set; } = [];
    public bool       SendStopOnDisconnect { get; set; }
}