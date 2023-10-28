using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Commands;

namespace DCCRailway.System.Adapters;

public interface IAdapter {
    bool IsConnected { get; }
    void Connect();
    void Disconnect();

    void    SendData(byte[]   data, ICommand command);
    byte[]? RecvData(ICommand command);

    event EventHandler<StateChangedArgs> ConnectionStatusChanged;
    event EventHandler<DataRecvArgs>     DataReceived;
    event EventHandler<DataSentArgs>     DataSent;
    event EventHandler<ErrorArgs>        ErrorOccurred;
}