using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Events;
using DCCRailway.Controller.Helpers;

namespace DCCRailway.Controller.Adapters.Base;

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