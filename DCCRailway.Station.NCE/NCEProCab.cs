using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Station.NCE;

[Controller("NCEProCab", "North Coast Engineering (NCE)", "ProCab", "1.3")]
public class NceProCab : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

}