using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdIgnore(ILogger logger, WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.ForContext<WiThrottleServer>().Information("{0}:{2}=>'{1}'", ToString(), commandStr, connection.ToString());
    }

    public override string ToString() => "CMD:Ignore";
}