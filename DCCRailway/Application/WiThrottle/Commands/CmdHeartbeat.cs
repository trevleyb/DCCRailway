using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdHeartBeat : ThrottleCmd, IThrottleCmd {
    public CmdHeartBeat(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) : base(connection, cmdString, ref options) => connection.LastCommand = this;

    public IServerMsg Execute() {
        switch (CmdString) {
        case "+":
            Logger.Log.Information($"Received a HEARTBEAT + (ON) command from '{Connection.ThrottleName}'");
            Connection.HeartbeatState = HeartbeatStateEnum.On;
            break;
        case "-":
            Logger.Log.Information($"Received a HEARTBEAT - (OFF) command from '{Connection.ThrottleName}'");
            Connection.HeartbeatState = HeartbeatStateEnum.Off;
            break;
        default:
            Logger.Log.Information($"Received a HEARTBEAT from '{Connection.ThrottleName}'");
            break;
        }
        return new MsgComplete();
    }

    public override string ToString() => "COMMAND: HEARTBEAT";
}