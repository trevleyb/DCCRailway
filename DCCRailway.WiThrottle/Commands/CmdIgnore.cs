using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdIgnore(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd
{
    public void Execute(string commandStr)
    {
        logger.Information("WiThrottle Recieved Cmd from {0}: Ignore - {1}:{3}=>'{2}'", connection.ConnectionHandle,
                           ToString(), commandStr, connection.ToString());
    }

    public override string ToString() => "CMD:Ignore";
}