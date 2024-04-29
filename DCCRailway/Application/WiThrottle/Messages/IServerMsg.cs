using System.Net.Sockets;

namespace DCCRailway.Application.WiThrottle.Messages;

public interface IServerMsg {
    void Execute(NetworkStream stream);
}