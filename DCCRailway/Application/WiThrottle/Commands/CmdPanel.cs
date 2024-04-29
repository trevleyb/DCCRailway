using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdPanel (WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd(connection, options), IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
    }
    public override string ToString() => "CMD:Panel";
}