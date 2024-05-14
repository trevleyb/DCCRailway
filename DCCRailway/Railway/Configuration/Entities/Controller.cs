using System.Diagnostics;
using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
[DebuggerDisplay("DCC COMMAND STATION CONTROLLER={Name}")]
public class Controller() {
    public string       Name                 { get; set; }
    public Adapters     Adapters             { get; set; } = new();
    public Tasks        Tasks                { get; set; } = [];
    public Parameters   Parameters           { get; set; } = [];
    public bool         SendStopOnDisconnect { get; set; }
}