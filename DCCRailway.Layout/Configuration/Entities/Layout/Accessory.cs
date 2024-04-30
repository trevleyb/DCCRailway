using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("ACCESSORY={Id}, Name: {Name}")]
public class Accessory : BaseEntityDecoder {
    public Accessory(string id = "") : base(id) {
        AddressType = DCCAddressType.Accessory;
    }
}