using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdQuit : ThrottleCmd, IThrottleCmd {
    public CmdQuit(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) : base(connection, cmdString, ref options) => connection.LastCommand = this;

    public IServerMsg Execute() {
        Logger.Log.Information($"Received a QUIT command from '{Connection.ConnectionID}'");
        return new MsgQuit();
    }

    public override string ToString() => "COMMAND: QUIT";
}