using DCCRailway.Layout.Adapters.Events;
using DCCRailway.Layout.Commands;

namespace DCCRailway.Layout.Adapters;

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