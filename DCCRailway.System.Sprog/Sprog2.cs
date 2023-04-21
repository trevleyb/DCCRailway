using System;
using System.Collections.Generic;
using DCCRailway.Core.Attributes;
using DCCRailway.Core.Systems;
using DCCRailway.Core.Systems.Adapters.Events;
using DCCRailway.Core.Systems.Attributes;
using DCCRailway.Core.Systems.Types;
using DCCRailway.Core.Utilities;
using DCCRailway.Systems.Virtual;

namespace DCCRailway.Systems.Sprog; 

[System("Sprog II", "DCCSystems", "Sprog II")]
public class Sprog2 : Core.Systems.System, ISystem {
    public override IDCCAddress CreateAddress() {
        return new DCCAddress();
    }

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
        return new DCCAddress(address, type);
    }

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<SprogVirtualAdapter>();
    }
    
    protected override void RegisterCommands() {
        //RegisterCommand<IDummyCmd> (typeof (Commands.VirtualDummy));
        //RegisterCommand<ICmdStatus> (typeof (Commands.VirtualStatus));
    }

    #region Manage the events from the Adapter

    protected override void Adapter_ErrorOccurred(object? sender, ErrorArgs e) {
        Logger.Log.Debug("Error occurred in the Adapter: " + e);
    }

    protected override void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) {
        Logger.Log.Debug("A state change event occurred in the Adapter: " + e.EventType);
    }

    protected override void Adapter_DataSent(object? sender, DataSentArgs e) {
        Logger.Log.Debug("Data was sent to the Adapter: " + e.Data?.ToDisplayValues());
    }

    protected override void Adapter_DataReceived(object? sender, DataRecvArgs e) {
        Logger.Log.Debug("Data was recieved from the Adapter: " + e.Data?.ToDisplayValues());
    }

    #endregion
}