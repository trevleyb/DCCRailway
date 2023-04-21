using System;
using System.Collections.Generic;
using DCCRailway.Core.Attributes;
using DCCRailway.Core.Systems;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Adapters.Events;
using DCCRailway.Core.Systems.Attributes;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Systems.NCE.Adapters;

namespace DCCRailway.Systems.NCE; 

[System("NCEProCab", "North Coast Engineering (NCE)", "ProCab", "1.3")]
public class NceProCab : Core.Systems.System, ISystem {
    public override IDCCAddress CreateAddress() {
        return new DCCAddress();
    }

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<NCESerial>();
        RegisterAdapter<NCEUSBSerial>();
        RegisterAdapter<NCEVirtualAdapter>();
    }
    
    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
        return new DCCAddress(address, type);
    }
    
    protected override void RegisterCommands() {
        throw new NotImplementedException();
    }

    #region Manage the events from the Adapter

    protected override void Adapter_ErrorOccurred(object? sender, ErrorArgs e) {
        throw new NotImplementedException();
    }

    protected override void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) {
        throw new NotImplementedException();
    }

    protected override void Adapter_DataSent(object? sender, DataSentArgs e) {
        throw new NotImplementedException();
    }

    protected override void Adapter_DataReceived(object? sender, DataRecvArgs e) {
        throw new NotImplementedException();
    }

    #endregion
}