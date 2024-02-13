using System;
using DCCRailway.System.Layout.Controllers;
using DCCRailway.System.Layout.Types;
using DCCRailway.System.NCE.Adapters;

namespace DCCRailway.System.NCE;

[Controller("NCEProCab", "North Coast Engineering (NCE)", "ProCab", "1.3")]
public class NceProCab : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<NCESerial>();
        RegisterAdapter<NCEUSBSerial>();
        RegisterAdapter<NCEVirtualAdapter>();
    }

    protected override void RegisterCommands() => throw new NotImplementedException();
}