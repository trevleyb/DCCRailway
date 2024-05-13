using System.Diagnostics;
using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
[DebuggerDisplay("DCC COMMAND STATION CONTROLLER={Name}")]
public class DCCController() {
    public string       Name                 { get; set; }
    public DCCAdapter   Adapter              { get; set; } = new();
    public DCCTasks     Tasks                { get; set; } = [];
    public Parameters   Parameters           { get; set; } = [];

    public bool         SendStopOnDisconnect { get; set; }
}