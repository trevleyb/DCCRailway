using DCCRailway.Common.Types;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.Controller.Virtual;

[Controller("Virtual", "Virtual", "Virtual")]
public class VirtualCommandStation : CommandStation, ICommandStation {
    public override DCCAddress CreateAddress()                                                       => new();
    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new(address, type);
}