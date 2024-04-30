using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("SENSOR={Id}, Name: {Name}")]
public class Sensor : BaseEntityDecoder {
    public Sensor(string id = "") : base(id) {
        AddressType = DCCAddressType.Sensor;
    }
}