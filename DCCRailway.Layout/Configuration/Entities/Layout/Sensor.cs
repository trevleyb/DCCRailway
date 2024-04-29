using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("SENSOR={Id}, Name: {Name}")]
public class Sensor(string id = "") : BaseEntityDecoder(id, DCCAddressType.Sensor) { }