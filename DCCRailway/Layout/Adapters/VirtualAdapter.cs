using DCCRailway.Layout.Commands;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.Adapters;

public abstract class VirtualAdapter : Adapter, IAdapter {
    public bool IsConnected { get; set; }
    public virtual void Connect() {
        if (IsConnected) Disconnect();
    }

    public virtual void Disconnect() {
        IsConnected = false;
    }
    
    public void SendData(byte[] data, ICommand command) {
        Logger.Log.Debug($"Sending Data to the Adapter: '{command.Info().Name} => {data.FromByteArray()}'");
    }
    
    public byte[]? RecvData(ICommand command) {
        Logger.Log.Debug($"Receiving Data from the Adapter: '{command.Info().Name}'");
        return new byte[] { 0x00 };
    }
}