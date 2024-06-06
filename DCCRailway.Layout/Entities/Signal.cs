using System.Diagnostics;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

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