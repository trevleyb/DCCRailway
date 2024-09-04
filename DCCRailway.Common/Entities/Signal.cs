using System.Diagnostics;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("SIGNAL={Id}, Name: {Name}")]
public class Signal : LayoutEntityDecoder {
    public Signal(string id = "") : base(id) { }

    public new DCCAddress Address {
        get => base.Address;
        set {
            base.Address             = value;
            base.Address.AddressType = DCCAddressType.Signal;
        }
    }
}