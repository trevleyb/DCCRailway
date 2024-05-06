using DCCRailway.Common.Helpers;

namespace DCCRailway.Throttles.WiThrottle.Commands;

public class CmdIgnore (WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}:{2}=>'{1}'",ToString(),commandStr,connection.ToString());
    }
    public override string ToString() => $"CMD:Ignore";
}