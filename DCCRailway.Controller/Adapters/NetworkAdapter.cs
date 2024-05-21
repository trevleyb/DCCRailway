using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;
using Serilog;

namespace DCCRailway.Controller.Adapters;

public abstract class NetworkAdapter(ILogger logger) : Adapter, IAdapter {
    public bool IsConnected => throw new NotImplementedException();

    public void Connect() => throw new NotImplementedException();

    public void Disconnect() => throw new NotImplementedException();

    public byte[]? RecvData(ICommand? command = null) => throw new NotImplementedException();

    public void SendData(byte[] data, ICommand? command = null) => throw new NotImplementedException();

    public void Dispose() {
        logger.Verbose("NetworkAdapter Disposed.");
    }
}