using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.System;

public class Manufacturer  {
    public byte Id { get; set; }
    public string Name { get; set; }

    public Manufacturer(byte id,string name) {
        Id          = id;
        Name        = name;
    }
}