using System.Diagnostics;
using DCCRailway.Common.Entities.Base;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("MANUFACTURER={Id}, Name: {Name}")]
public class Manufacturer(string id, string name) : LayoutEntity(id, name);