using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdUnknown : ThrottleCmd, IThrottleCmd {
    public CmdUnknown(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;

    public string? Execute() {
        Logger.Log.Information($"Received an unknown command from a throttle: '{ConnectionEntry.ConnectionID}' of '{CmdString}'");
        return null;
    }

    public override string ToString() => "COMMAND: UNKNOWN";
}