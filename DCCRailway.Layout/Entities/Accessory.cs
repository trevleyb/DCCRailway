using System.Diagnostics;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

[Serializable]
[DebuggerDisplay("ACCESSORY={Id}, Name: {Name}")]
public class Accessory(string id = "") : LayoutEntityDecoder(id) {
    public new DCCAddress Address {
        get => base.Address;
        set {
            base.Address             = value;
            base.Address.AddressType = DCCAddressType.Accessory;
        }
    }
}