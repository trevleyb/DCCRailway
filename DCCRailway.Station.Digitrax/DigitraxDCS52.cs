using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Station.Digitrax;

[Controller("DCS52", "DigiTrax", "DCS52")]
public class DCS52 : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}