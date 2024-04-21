using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Station.Sprog;

[Controller("Sprog II", "DCCSystems", "Sprog II")]
public class Sprog2 : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}