using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdIgnore(ILogger logger, WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.Information("WiThrottle Cmd: Ignore - {0}:{2}=>'{1}'", ToString(), commandStr, connection.ToString());
    }

    public override string ToString() => "CMD:Ignore";
}