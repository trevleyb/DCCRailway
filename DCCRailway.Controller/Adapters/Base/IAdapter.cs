using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Events;

namespace DCCRailway.Controller.Adapters.Base;

public interface IAdapter : IParameterMappable {
    bool IsConnected { get; }
    void Connect();
    void Disconnect();
    void Dispose();

    void    SendData(string data, ICommand? command);
    void    SendData(byte[] data, ICommand? command);
    byte[]? RecvData(ICommand? command = null);

    event EventHandler<DataRecvArgs>  DataReceived;
    event EventHandler<DataSentArgs>  DataSent;
    event EventHandler<DataErrorArgs> ErrorOccurred;
}