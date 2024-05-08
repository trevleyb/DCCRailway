using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Sprog;

[Controller("Sprog II", "DCCSystems", "Sprog II")]
public class Sprog2 : Controller, IController {
    public override DCCAddress CreateAddress() => new DCCAddress();
    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}