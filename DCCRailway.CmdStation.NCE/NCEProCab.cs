using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE;

[Controller("NCEProCab", "North Coast Engineering (NCE)", "ProCab", "1.3")]
public class NceProCab : Controller, IController {
    public override DCCAddress CreateAddress() => new DCCAddress();
    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

}