using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Digitrax;

[Controller("DCS52", "DigiTrax", "DCS52")]
public class DCS52 : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}