using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;
public class Accessory : ConfigWithDecoder {
    public Accessory() : base(DCCAddressType.Accessory) { } 
}