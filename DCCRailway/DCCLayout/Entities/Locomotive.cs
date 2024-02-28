using DCCRailway.DCCController.Types;
using DCCRailway.DCCLayout.Entities.Base;

namespace DCCRailway.DCCLayout.Entities;
public class Locomotive() : ConfigWithDecoder(DCCAddressType.Long) {
    
    public Locomotive(IDCCAddress address, DCCDirection direction = DCCDirection.Forward) : this() {
        Address   = address;
        Direction = direction;
        Speed.Speed = 0;
    }

    public Locomotive(int address, DCCAddressType type = DCCAddressType.Long, DCCDirection direction = DCCDirection.Stop) : this() {
        Address         = new DCCAddress(address, type);
        Direction = direction;
        Speed.Speed     = 0;
    }
    
    public string Type { get; set; }
    public string RoadName { get; set; }
    public string RoadNumber { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }

    public DCCSpeed          Speed          { get; set; } = new DCCSpeed(0);
    public DCCDirection      Direction      { get; set; } = DCCDirection.Stop;
    public DCCFunctionBlocks FunctionBlocks { get; set; } = new DCCFunctionBlocks();
    public new string ToString() => $"{Name}";
}