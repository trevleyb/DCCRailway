using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdHeartbeat (WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd(connection, options), IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        if (commandStr.Length <= 1) return;
        switch (commandStr[1]) {
        case '+':
            Logger.Log.Information($"Received a HEARTBEAT + (ON) command from '{Connection.ThrottleName}'");
            Connection.HeartbeatState = HeartbeatStateEnum.On;
            Connection.AddResponseMsg(new MsgHeartbeat(Connection, Options));
            break;
        case '-':
            Logger.Log.Information($"Received a HEARTBEAT - (OFF) command from '{Connection.ThrottleName}'");
            Connection.HeartbeatState = HeartbeatStateEnum.Off;
            break;
        default:
            Logger.Log.Information($"Received a HEARTBEAT from '{Connection.ThrottleName}'");
            break;
        }

    }
    public override string ToString() => "CMD:Heartbeat";
}