using DCCRailway.Common.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle.Commands;

public class CmdRoster(WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'", ToString(), commandStr);
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
                Logger.Log.Information("{0}:{2}=>Unknown Panel Command recieved=>'{1}'", ToString(), commandStr, connection.ToString());
                break;
            }
        } catch {
            Logger.Log.Error("{0}:{2}=>Unable to Process the command =>'{1}'", ToString(), commandStr, connection.ToString());
        }
    }

    public override string ToString() => "CMD:Roster";
}