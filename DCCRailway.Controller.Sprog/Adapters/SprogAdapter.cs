using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Adapters.Events;
using DCCRailway.Controller.Attributes;
using Serilog;
using Serilog.Core;

namespace DCCRailway.Controller.Sprog.Adapters;

[Adapter("Sprog", AdapterType.Virtual)]
public class SprogAdapter(ILogger logger) : Adapter, IAdapter {
    private byte[] _lastCommand;

    public bool IsConnected { get; private set; }

    public void Connect() {
        logger.Debug("Connecting to the Virtual Adapter");
        IsConnected = true;
    }

    public void Disconnect() {
        logger.Debug("Disconnecting from the Virtual Adapter");
        IsConnected = false;
    }

    public void Dispose() { }

    public byte[]? RecvData(ICommand? command = null) {
        logger.Debug("Listening for data from the Adapter: '" + _lastCommand.FromByteArray() + "'");

        var result = _lastCommand.FromByteArray() switch {
            "STATUS_COMMAND" => "OK".ToByteArray(),
            "DUMMY_COMMAND"  => "OK".ToByteArray(),
            _                => null
        };
        logger.Debug("Data to return: '" + result.FromByteArray() + "'");
        OnDataRecieved(new DataRecvArgs(result, this, command));

        return result;
    }

    public void SendData(byte[] data, ICommand? command = null) {
        logger.Debug("Sending data to the Adapter");
        _lastCommand = data;
        logger.Debug("Data Sent: " + data.FromByteArray());
        OnDataSent(new DataSentArgs(data, this, command));
    }
}