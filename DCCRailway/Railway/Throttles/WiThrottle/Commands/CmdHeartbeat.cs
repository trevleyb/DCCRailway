using DCCRailway.Common.Helpers;
using Serilog;

namespace DCCRailway.Railway.Throttles.WiThrottle.Commands;

public class CmdHeartbeat(ILogger logger, WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.ForContext<WiThrottleServer>().Information("{0}:{2}=>'{1}'", ToString(), commandStr, connection.ToString());
        if (commandStr.Length <= 1) return;
        switch (commandStr[1]) {
        case '+':
            connection.HeartbeatState = HeartbeatStateEnum.On;
            break;
        case '-':
            connection.HeartbeatState = HeartbeatStateEnum.Off;
            break;
        default:
            logger.ForContext<WiThrottleServer>().Verbose("{0}:{1}=>Heartbeat Receievd'", ToString(), commandStr);
            break;
        }
    }

    public override string ToString() => "CMD:Heartbeat";
}