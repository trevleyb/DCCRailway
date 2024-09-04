using System.Diagnostics;
using DCCRailway.Common.Entities.Base;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("SENSOR={Id}, Name: {Name}")]
public class Sensor : LayoutEntityDecoder {
    public Sensor(string id = "") : base(id) { }
}