using System.Net.Sockets;
using System.Text;
using Serilog;

namespace DCCRailway.Application.WiThrottle.Messages;

public abstract class ServerMsg : IServerMsg {

    protected readonly WiThrottleConnection    Connection;
    protected          WiThrottleServerOptions Options;

    protected ServerMsg(WiThrottleConnection connection, ref WiThrottleServerOptions options) {
        Options    = options;
        Connection = connection;
        Connection.UpdateHeartbeat();
    }
    public abstract void Execute(NetworkStream stream);

    protected void SendMessage(NetworkStream stream, string message) {
        try {
            var serverMessage = Encoding.ASCII.GetBytes(message + Options.Terminator);
            stream.Write(serverMessage, 0, serverMessage.Length);
        }
        catch (Exception ex) {
            Log.Error("Unable to send message to the client : {0}", ex.Message);
            throw;
        }
    }
}