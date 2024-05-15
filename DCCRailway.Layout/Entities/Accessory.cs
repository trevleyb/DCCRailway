using System.Diagnostics;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Entities;

[Serializable, DebuggerDisplay("ACCESSORY={Id}, Name: {Name}")]
public class Accessory : LayoutEntityDecoder {
    public Accessory(string id = "") : base(id) { }
}