using DCCRailway.System.Commands;

namespace DCCRailway.System.Adapters;

public abstract class NetworkAdapter : Adapter, IAdapter {
    public bool IsConnected => throw new NotImplementedException();

    public void Connect() {
        throw new NotImplementedException();
    }

    public void Disconnect() {
        throw new NotImplementedException();
    }

    public byte[]? RecvData(ICommand? command = null) {
        throw new NotImplementedException();
    }

    public void SendData(byte[] data, ICommand? command = null) {
        throw new NotImplementedException();
    }
}