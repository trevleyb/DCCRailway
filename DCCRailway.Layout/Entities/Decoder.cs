using System.Diagnostics;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Entities;

[Serializable, DebuggerDisplay("DECODER={Id}, Name: {Name}, Manufacturers: {Manufacturer}")]
public class Decoder(string id = "") : LayoutEntity(id) {
    public byte    Manufacturer { get; set; }
    public string? Model        { get; set; }
    public string? Family       { get; set; }
}