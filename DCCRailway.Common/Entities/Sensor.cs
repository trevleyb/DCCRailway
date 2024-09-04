using System.Diagnostics;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("SENSOR={Id}, Name: {Name}")]
public class Sensor(string id = "") : LayoutEntityDecoder(id, DCCAddressType.Sensor);