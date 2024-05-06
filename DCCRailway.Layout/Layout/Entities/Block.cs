using System.Diagnostics;
using DCCRailway.Layout.Layout.Base;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
[DebuggerDisplay("BLOCK={Id}, Name: {Name}")]
public class Block(string id = "") : LayoutEntity(id) { }