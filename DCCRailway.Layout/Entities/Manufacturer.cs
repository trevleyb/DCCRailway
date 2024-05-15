using System.Diagnostics;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Entities;

[Serializable, DebuggerDisplay("MANUFACTURER={Id}, Name: {Name}")]
public class Manufacturer(string id, string name) : LayoutEntity(id, name);