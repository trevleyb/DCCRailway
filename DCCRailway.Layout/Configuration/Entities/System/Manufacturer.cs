using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.System;

public class Manufacturer : BaseEntity {
    public byte Identifier { get; set; }

    public Manufacturer(string name, byte identifier) {
        Name        = name;
        Description = name;
        Identifier  = identifier;
    }
}