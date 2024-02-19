﻿
using DCCRailway.Configuration.Base;
using DCCRailway.Layout.Types;

namespace DCCRailway.Configuration.Entities;
public class Locomotive() : ConfigWithDecoder(DCCAddressType.Long) {
    
    public Locomotive(IDCCAddress address, DCCDirection direction = DCCDirection.Forward) : this() {
        Address   = address;
        Direction = direction;
    }

    public Locomotive(int address, DCCAddressType type = DCCAddressType.Long, DCCDirection direction = DCCDirection.Stop) : this() {
        Address   = new DCCAddress(address, type);
        Direction = direction;
    }
    
    public string Type { get; set; }
    public string RoadName { get; set; }
    public string RoadNumber { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }

    public DCCSpeed     Speed     { get; set; } = new DCCSpeed(0);
    public IDCCAddress  Address   { get; set; }
    public DCCDirection Direction { get; set; }
    public new string ToString() => $"{Name}";
}