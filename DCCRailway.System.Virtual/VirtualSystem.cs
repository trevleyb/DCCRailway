using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;
using DCCRailway.System.Virtual.Adapters;
using DCCRailway.System.Virtual.Commands;

namespace DCCRailway.System.Virtual;

[System("Virtual", "Virtual", "Virtual")]
public class VirtualSystem : System, ISystem {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<VirtualAdapter>();
    }

    protected override void RegisterCommands() {
        RegisterCommand<IDummyCmd>(typeof(VirtualDummy));
        RegisterCommand<ICmdStatus>(typeof(VirtualStatus));
    }

    #region Manage the events from the Adapter
    protected override void Adapter_ErrorOccurred(object? sender, ErrorArgs e) => Logger.Log.Debug("Error occurred in the Adapter: " + e);

    protected override void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) => Logger.Log.Debug("A state change event occurred in the Adapter: " + e.EventType);

    protected override void Adapter_DataSent(object? sender, DataSentArgs e) => Logger.Log.Debug("Data was sent to the Adapter: " + e.Data?.ToDisplayValues());

    protected override void Adapter_DataReceived(object? sender, DataRecvArgs e) => Logger.Log.Debug("Data was recieved from the Adapter: " + e.Data?.ToDisplayValues());
    #endregion
}