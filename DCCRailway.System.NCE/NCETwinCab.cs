using System;
using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.NCE.Adapters;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE;

[System("NCETwinCab", "North Coast Engineering (NCE)", "TwinCab", "1.1")]
public class NceTwinCab : System, ISystem {
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