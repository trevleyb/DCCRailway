using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Virtual;

[Adapter("Dixitrax")]
public class DigitraxVirtualAdapter : Adapter, IAdapter {
    private byte[] _lastCommand;

    public bool IsConnected { get; private set; }

    public void Connect() {
        Logger.Log.Debug("Connecting to the Virtual Adapter");
        IsConnected = true;
    }

    public void Disconnect() {
        Logger.Log.Debug("Disconnecting from the Virtual Adapter");
        IsConnected = false;
    }

    public byte[]? RecvData(ICommand? command = null) {
        Logger.Log.Debug("Listening for data from the Adapter: '" + _lastCommand.FromByteArray() + "'");

        var result = _lastCommand.FromByteArray() switch {
            "STATUS_COMMAND" => "OK".ToByteArray(),
            "DUMMY_COMMAND" => "OK".ToByteArray(),
            _ => null
        };
        Logger.Log.Debug("Data to return: '" + result.FromByteArray() + "'");
        OnDataRecieved(new DataRecvArgs(result, this, command));

        return result;
    }

    public void SendData(byte[] data, ICommand? command = null) {
        Logger.Log.Debug("Sending data to the Adapter");
        _lastCommand = data;
        Logger.Log.Debug("Data Sent: " + data.FromByteArray());
        OnDataSent(new DataSentArgs(data, this, command));
    }
}