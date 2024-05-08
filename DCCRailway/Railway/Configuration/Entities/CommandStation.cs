using System.Diagnostics;
using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
[DebuggerDisplay("DCC COMMAND STATION CONTROLLER={Name}")]
public class CommandStation() {
    public string  Name                 { get; set; }
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
    public bool    IsActive             { get; set; }
    public DCCPowerState PowerState     { get; set; }
    public Parameters Parameters { get; set; } = [];
}