using DCCRailway.Common.Types;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.Controller.NCE;

[Controller("NCEProCab", "North Coast Engineering (NCE)", "ProCab", "1.3")]
public class NceProCab : CommandStation, ICommandStation {
    public override DCCAddress CreateAddress() => new DCCAddress();
    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

}