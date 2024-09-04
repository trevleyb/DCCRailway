using System.Diagnostics;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("SIGNAL={Id}, Name: {Name}")]
public class Signal(string id = "") : LayoutEntityDecoder(id, DCCAddressType.Signal) { }