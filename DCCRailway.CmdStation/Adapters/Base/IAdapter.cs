using DCCRailway.CmdStation.Adapters.Events;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Helpers;

namespace DCCRailway.CmdStation.Adapters.Base;

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