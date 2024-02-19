using DCCRailway.Configuration.Base;
using DCCRailway.System.Types;

namespace DCCRailway.Configuration.Entities;
public class Accessory : ConfigWithDecoder {
    public Accessory() : base(DCCAddressType.Accessory) { } 
}