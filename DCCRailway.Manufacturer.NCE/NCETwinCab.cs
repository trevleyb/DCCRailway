using System;
using DCCRailway.Manufacturer.NCE.Adapters;
using DCCRailway.System.Controllers;
using DCCRailway.System.Types;

namespace DCCRailway.Manufacturer.NCE;

[Controller("NCETwinCab", "North Coast Engineering (NCE)", "TwinCab", "1.1")]
public class NceTwinCab : Controller, IController {
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