using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdRoster (WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
    }
    public override string ToString() => "CMD:Roster";
}