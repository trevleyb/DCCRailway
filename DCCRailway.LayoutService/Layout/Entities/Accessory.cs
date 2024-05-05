using System.Diagnostics;
using DCCRailway.Common.Types;
using DCCRailway.LayoutService.Layout.Base;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
[DebuggerDisplay("ACCESSORY={Id}, Name: {Name}")]
public class Accessory : LayoutEntityDecoder {
    public Accessory(string id = "") : base(id) {
        AddressType = DCCAddressType.Accessory;
    }
}