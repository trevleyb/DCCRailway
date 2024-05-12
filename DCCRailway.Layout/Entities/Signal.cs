using System.Diagnostics;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Entities;

[Serializable]
[DebuggerDisplay("SIGNAL={Id}, Name: {Name}")]
public class Signal : LayoutEntityDecoder {
    public Signal(string id = "") : base(id) {
        AddressType = DCCAddressType.Signal;
    }
}