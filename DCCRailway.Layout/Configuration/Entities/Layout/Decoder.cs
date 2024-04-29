using System.Diagnostics;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("DECODER={Id}, Name: {Name}, Manufacturers: {Manufacturer}")]
public class Decoder (string id = "") : BaseEntity(id) {
    public byte     Manufacturer { get; set; }
    public string?  Model        { get; set; }
    public string?  Family       { get; set; }
}