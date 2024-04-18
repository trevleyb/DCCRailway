using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Sensor(Guid id) : BaseEntityDecoder(id, DCCAddressType.Sensor) {
    public Sensor() : this(Guid.NewGuid()) { }
}