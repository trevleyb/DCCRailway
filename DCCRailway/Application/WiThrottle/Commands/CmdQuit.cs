using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdQuit (WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
    }
    public override string ToString() => $"CMD:Quit [{connection?.ConnectionID ?? 0}]";
}