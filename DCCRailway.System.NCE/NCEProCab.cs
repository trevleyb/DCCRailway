using System;
using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.NCE.Adapters;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE;

[System("NCEProCab", "North Coast Engineering (NCE)", "ProCab", "1.3")]
public class NceProCab : System, ISystem {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<NCESerial>();
        RegisterAdapter<NCEUSBSerial>();
        RegisterAdapter<NCEVirtualAdapter>();
    }

    protected override void RegisterCommands() => throw new NotImplementedException();

    #region Manage the events from the Adapter
    protected override void Adapter_ErrorOccurred(object? sender, ErrorArgs e) => throw new NotImplementedException();

    protected override void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) => throw new NotImplementedException();

    protected override void Adapter_DataSent(object? sender, DataSentArgs e) => throw new NotImplementedException();

    protected override void Adapter_DataReceived(object? sender, DataRecvArgs e) => throw new NotImplementedException();
    #endregion
}