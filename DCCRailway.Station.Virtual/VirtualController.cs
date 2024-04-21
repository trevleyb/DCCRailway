using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Station.Virtual;

[Controller("Virtual", "Virtual", "Virtual")]
public class VirtualController : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}