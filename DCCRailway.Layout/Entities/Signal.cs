using System.Diagnostics;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

[Serializable]
[DebuggerDisplay("SIGNAL={Id}, Name: {Name}")]
public class Signal : LayoutEntityDecoder {
    public Signal(string id = "") : base(id) { }
}