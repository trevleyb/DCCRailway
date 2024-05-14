using System.Diagnostics;
using DCCRailway.Layout.Base;

namespace DCCRailway.Railway.Configuration.Helpers;

[Serializable]
[DebuggerDisplay("MANUFACTURER={Id}, Name: {Name}")]
public class Manufacturer(string id, string name) : LayoutEntity(id,name);