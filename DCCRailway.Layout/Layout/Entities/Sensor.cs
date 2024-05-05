using System.Diagnostics;
using DCCRailway.Common.Types;
using DCCRailway.LayoutService.Layout.Base;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
[DebuggerDisplay("SENSOR={Id}, Name: {Name}")]
public class Sensor : LayoutEntityDecoder {
    public Sensor(string id = "") : base(id) {
        AddressType = DCCAddressType.Sensor;
    }
}