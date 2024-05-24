using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdQuit(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd from [{0}]: Quit - {1}:{3}=>'{2}'", connection.ConnectionHandle, ToString(), commandStr, connection.ToString());
    }

    public override string ToString() {
        return "CMD:Quit";
    }
}