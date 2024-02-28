using DCCRailway.DCCController.Commands;

namespace DCCRailway.DCCController.Adapters;

public abstract class USBAdapter : Adapter, IAdapter {
    public bool IsConnected => throw new NotImplementedException();

    public void Connect() => throw new NotImplementedException();

    public void Disconnect() => throw new NotImplementedException();

    public byte[]? RecvData(ICommand? command = null) => throw new NotImplementedException();

    public void SendData(byte[] data, ICommand? command = null) => throw new NotImplementedException();
}