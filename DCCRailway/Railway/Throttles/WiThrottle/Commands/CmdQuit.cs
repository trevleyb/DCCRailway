using DCCRailway.Common.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle.Commands;

public class CmdQuit (WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}:{2}=>'{1}'",ToString(),commandStr,connection.ToString());
    }
    public override string ToString() => $"CMD:Quit";
}