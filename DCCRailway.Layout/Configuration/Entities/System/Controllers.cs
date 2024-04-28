using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Collection;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
public class Controllers(string prefix = "CMD") : EntityCollection<Controller>(prefix) { }