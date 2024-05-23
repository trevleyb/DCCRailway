using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdRoster(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd from [{0}]: Roster - {1}=>'{2}'", connection.ConnectionHandle,
                           ToString(), commandStr);
        try {
            var cmd = commandStr[..3];
            switch (cmd.ToUpper()) {
            case "RCP": // RE-ORDER positions
                break;
            case "RCR": // REMOVE DAC
                break;
            case "RC+": // Add
                break;
            case "RC-": // Remove
                break;
            case "RCF": // Functions
                break;
            default:
                logger.ForContext<Server>().Information("{0}:{2}=>Unknown Panel Command recieved=>'{1}'", ToString(),
                                                        commandStr, connection.ToString());
                break;
            }
        }
        catch {
            logger.ForContext<Server>().Error("{0}:{2}=>Unable to Process the command =>'{1}'", ToString(), commandStr,
                                              connection.ToString());
        }
    }

    public override string ToString() {
        return "CMD:Roster";
    }
}