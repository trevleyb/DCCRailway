using System;
using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Controllers;
using DCCRailway.System.NCE.Adapters;

namespace DCCRailway.System.NCE;

[Controller("NCEProCab", "North Coast Engineering (NCE)", "ProCab", "1.3")]
public class NceProCab : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

}