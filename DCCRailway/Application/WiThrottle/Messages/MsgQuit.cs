using System.Net.Sockets;
using Microsoft.AspNetCore.Hosting.Server;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgQuit : IServerMsg {
    public void Execute(NetworkStream stream) { }
}