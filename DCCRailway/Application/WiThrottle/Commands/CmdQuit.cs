using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdQuit : ThrottleCmd, IThrottleCmd {
    public CmdQuit(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;

    public string? Execute() {
        Logger.Log.Information($"Received a QUIT command from '{ConnectionEntry.ConnectionID}'");

        return null;
    }

    public override string ToString() => "COMMAND: QUIT";
}