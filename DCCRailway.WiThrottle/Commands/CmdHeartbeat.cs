using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdHeartbeat(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd
{
    public void Execute(string commandStr)
    {
        logger.Information("WiThrottle Recieved Cmd: Heartbeat - {0}:{2}=>'{1}'", ToString(), commandStr,
                           connection.ToString());
        if (commandStr.Length <= 1) return;
        switch (commandStr[1])
        {
        case '+':
            connection.HeartbeatState = HeartbeatStateEnum.On;
            break;
        case '-':
            connection.HeartbeatState = HeartbeatStateEnum.Off;
            break;
        default:
            logger.Verbose("WiThrottle Recieved Cmd from {0}: Heartbeat - {1}:{2}=>Heartbeat Receievd'",
                           connection.ConnectionHandle, ToString(), commandStr);
            break;
        }
    }

    public override string ToString() => "CMD:Heartbeat";
}