using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
public class Controller : BaseEntity, IEntity {
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
    public bool    IsActive             { get; set; }
}