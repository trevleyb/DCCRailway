using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

public class Decoder (Guid id) : BaseEntity(id) {
    public byte     Manufacturer { get; set; }
    public string?  Model        { get; set; }
    public string?  Family       { get; set; }
}