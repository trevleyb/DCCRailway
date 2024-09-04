using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using Serilog;

namespace DCCRailway.Controller.Adapters;

[Adapter("Console", AdapterType.Virtual, "Adapter that writes to the Console", "1.0")]
public class NullAdapter(ILogger logger) : Adapter, IAdapter {
    public bool IsConnected                              => true;
    public void Connect()                                { }
    public void Disconnect()                             { }
    public void SendData(byte[] data, ICommand? command) { }

    public void SendData(string data, ICommand? commandReference = null) {
        SendData(data.ToByteArray(), commandReference);
    }

    public byte[]? RecvData(ICommand? command) {
        return [];
    }

    public void Dispose() {
        logger.Verbose("NullAdapter Disposed.");
    }
}