using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Common.Types;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
[DebuggerDisplay("CONTROLLER={Id}, Name: {Name}")]
public class Controller(string id = "") {
    public string  Name                 { get; set; }
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
    public bool    IsActive             { get; set; }
    public DCCPowerState PowerState     { get; set; }
    public Parameters Parameters { get; set; } = [];
}