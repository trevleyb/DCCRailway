using System.Diagnostics;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("ACCESSORY={Id}, Name: {Name}")]
public partial class Accessory(string id = "") : LayoutEntityDecoder(id, DCCAddressType.Accessory) { }