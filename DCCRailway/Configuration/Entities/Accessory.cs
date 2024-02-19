using DCCRailway.Configuration.Base;
using DCCRailway.Layout.Types;

namespace DCCRailway.Configuration.Entities;
public class Accessory : ConfigWithDecoder {
    public Accessory() : base(DCCAddressType.Accessory) { } 
}