using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdHeartbeat (WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}:{2}=>'{1}'",ToString(),commandStr,connection.ToString());
        if (commandStr.Length <= 1) return;
        switch (commandStr[1]) {
        case '+':
            connection.HeartbeatState = HeartbeatStateEnum.On;
            break;
        case '-':
            connection.HeartbeatState = HeartbeatStateEnum.Off;
            break;
        default:
            Logger.Log.Information("{0}:{1}=>Heartbeat Receievd'",ToString(),commandStr);
            break;
        }

    }
    public override string ToString() => $"CMD:Heartbeat";
}