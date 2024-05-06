using System.Diagnostics;
using DCCRailway.Common.Types;

namespace DCCRailway.Configuration.Entities;

[Serializable]
[DebuggerDisplay("CONTROLLER={Name}")]
public class Controller() {
    public string  Name                 { get; set; }
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
    public bool    IsActive             { get; set; }
    public DCCPowerState PowerState     { get; set; }
    public Parameters Parameters { get; set; } = [];
}