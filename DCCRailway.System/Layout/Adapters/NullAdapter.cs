using DCCRailway.System.Layout.Commands;

namespace DCCRailway.System.Layout.Adapters;

[Adapter("Console", AdapterType.Virtual, "Adapter that writes to the Console", "1.0")]
public class NullAdapter : Adapter, IAdapter {
    public bool    IsConnected => true;
    public void    Connect() { }
    public void    Disconnect(){ }
    public void    SendData(byte[] data, ICommand command) { }
    public byte[]? RecvData(ICommand command) => [];
}