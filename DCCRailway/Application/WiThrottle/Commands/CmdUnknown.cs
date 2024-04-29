using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdUnknown : ThrottleCmd, IThrottleCmd {
    public CmdUnknown(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) : base(connection, cmdString, ref options) => connection.LastCommand = this;

    public IServerMsg Execute() {
        Logger.Log.Information($"Received an unknown command from a throttle: '{Connection.ConnectionID}' of '{CmdString}'");
        return new MsgComplete();
    }

    public override string ToString() => "COMMAND: UNKNOWN";
}