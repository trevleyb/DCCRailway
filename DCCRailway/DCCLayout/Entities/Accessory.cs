using DCCRailway.DCCController.Types;
using DCCRailway.DCCLayout.Entities.Base;

namespace DCCRailway.DCCLayout.Entities;
public class Accessory : ConfigWithDecoder {
    public Accessory() : base(DCCAddressType.Accessory) { } 
}