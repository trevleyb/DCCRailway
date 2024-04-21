using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Station.NCE;

[Controller("NCETwinCab", "North Coast Engineering (NCE)", "TwinCab", "1.1")]
public class NceTwinCab : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);
}