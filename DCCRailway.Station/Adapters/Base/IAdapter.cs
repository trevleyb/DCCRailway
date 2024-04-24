using DCCRailway.Station.Adapters.Events;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Helpers;

namespace DCCRailway.Station.Adapters.Base;

public interface IAdapter : IParameterMappable {
    bool IsConnected { get; }
    void Connect();
    void Disconnect();
    void Dispose();

    void    SendData(byte[]  data, ICommand command);
    byte[]? RecvData(ICommand command);

    event EventHandler<DataRecvArgs>  DataReceived;
    event EventHandler<DataSentArgs>  DataSent;
    event EventHandler<DataErrorArgs>  ErrorOccurred;
}