using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("ACCESSORY={Id}, Name: {Name}")]
public class Accessory(string id = "") : BaseEntityDecoder(id, DCCAddressType.Accessory) { }