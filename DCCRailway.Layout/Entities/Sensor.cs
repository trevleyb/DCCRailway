using System.Diagnostics;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Entities;

[Serializable, DebuggerDisplay("SENSOR={Id}, Name: {Name}")]
public class Sensor : LayoutEntityDecoder {
    public Sensor(string id = "") : base(id) { }
}