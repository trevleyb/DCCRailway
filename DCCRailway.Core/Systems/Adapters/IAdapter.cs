using System;
using DCCRailway.Core.Systems.Adapters.Events;
using DCCRailway.Core.Systems.Commands;

namespace DCCRailway.Core.Systems.Adapters; 

public interface IAdapter {
    static string Name { get; }
    string Description { get; }
    bool IsConnected { get; }

    void Connect();
    void Disconnect();

    void SendData(byte[] data, ICommand command);
    byte[]? RecvData(ICommand command);

    event EventHandler<StateChangedArgs> ConnectionStatusChanged;
    event EventHandler<DataRecvArgs> DataReceived;
    event EventHandler<DataSentArgs> DataSent;
    event EventHandler<ErrorArgs> ErrorOccurred;
}