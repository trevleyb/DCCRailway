using System.Net.Sockets;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgComplete : IServerMsg {
    public void Execute(NetworkStream stream) { }
}