using System.Diagnostics;
using DCCRailway.LayoutService.Layout.Base;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
[DebuggerDisplay("BLOCK={Id}, Name: {Name}")]
public class Block(string id = "") : LayoutEntity(id) { }