using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.System;

public class Manufacturer : PropertyChangeBase, IEntity<byte> {
    public byte Id { get; set; }
    public string Name { get; set; }

    public Manufacturer(string name, byte identifier) {
        Id          = identifier;
        Name        = name;
    }
}