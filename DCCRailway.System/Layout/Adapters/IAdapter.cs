using DCCRailway.System.Layout.Adapters.Events;
using DCCRailway.System.Layout.Commands;

namespace DCCRailway.System.Layout.Adapters;

public interface IAdapter {
    bool IsConnected { get; }
    void Connect();
    void Disconnect();

    void    SendData(byte[]   data, ICommand command);
    byte[]? RecvData(ICommand command);

    event EventHandler<DataRecvArgs>     DataReceived;
    event EventHandler<DataSentArgs>     DataSent;
    event EventHandler<ErrorArgs>        ErrorOccurred;
}