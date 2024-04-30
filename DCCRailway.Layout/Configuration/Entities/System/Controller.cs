using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
[DebuggerDisplay("CONTROLLER={Id}, Name: {Name}")]
public class Controller(string id = "") : BaseEntity(id){
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
    public bool    IsActive             { get; set; }
}