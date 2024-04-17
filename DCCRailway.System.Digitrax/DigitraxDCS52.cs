using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Controllers;

namespace DCCRailway.System.Digitrax;

[Controller("DCS52", "DigiTrax", "DCS52")]
public class DCS52 : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}