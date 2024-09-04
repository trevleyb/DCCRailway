using System.Diagnostics;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

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