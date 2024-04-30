using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdHeartbeat (WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        if (commandStr.Length <= 1) return;
        switch (commandStr[1]) {
        case '+':
            Logger.Log.Information($"Received a HEARTBEAT + (ON) command from '{connection.ThrottleName}'");
            connection.HeartbeatState = HeartbeatStateEnum.On;
            connection.AddResponseMsg(new MsgHeartbeat(options));
            break;
        case '-':
            Logger.Log.Information($"Received a HEARTBEAT - (OFF) command from '{connection.ThrottleName}'");
            connection.HeartbeatState = HeartbeatStateEnum.Off;
            break;
        default:
            Logger.Log.Information($"Received a HEARTBEAT from '{connection.ThrottleName}'");
            break;
        }

    }
    public override string ToString() => "CMD:Heartbeat";
}