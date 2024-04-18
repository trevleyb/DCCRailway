using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Signal(Guid id) : BaseEntityDecoder(id, DCCAddressType.Signal) {
    public Signal() : this(Guid.NewGuid()) { }
}