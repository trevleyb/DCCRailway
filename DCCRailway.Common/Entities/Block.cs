using System.Diagnostics;
using DCCRailway.Common.Entities.Base;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("BLOCK={Id}, Name: {Name}")]
public class Block(string id = "") : LayoutEntity(id) { }