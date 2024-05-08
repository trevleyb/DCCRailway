using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE;

[Controller("NCETwinCab", "North Coast Engineering (NCE)", "TwinCab", "1.1")]
public class NceTwinCab : Controller, IController {
    public override DCCAddress CreateAddress() => new DCCAddress();
    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}