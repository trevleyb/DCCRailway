using System.Diagnostics;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

[Serializable]
[DebuggerDisplay("BLOCK={Id}, Name: {Name}")]
public class Block(string id = "") : LayoutEntity(id) { }