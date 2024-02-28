using DCCRailway.DCCController.Adapters.Events;
using DCCRailway.DCCController.Commands;

namespace DCCRailway.DCCController.Adapters;

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