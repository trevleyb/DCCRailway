﻿using System;
using System.Collections.Generic;
using DCCRailway.Core.Attributes;
using DCCRailway.Core.Systems;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Adapters.Events;
using DCCRailway.Core.Systems.Attributes;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Systems.NCE.Adapters;

namespace DCCRailway.Systems.NCE; 

[System("NCETwinCab", "North Coast Engineering (NCE)", "TwinCab", "1.1")]
public class NceTwinCab : Core.Systems.System, ISystem {
    public override IDCCAddress CreateAddress() {
        return new DCCAddress();
    }

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
        return new DCCAddress(address, type);
    }

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<NCESerial>();
        RegisterAdapter<NCEUSBSerial>();
        RegisterAdapter<NCEVirtualAdapter>();
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