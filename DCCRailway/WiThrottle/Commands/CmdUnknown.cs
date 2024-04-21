using DCCRailway.Common.Utilities;

namespace DCCRailway.Station.WiThrottle.Commands;

public class CmdUnknown : ThrottleCmdBase, IThrottleCmd {
    public CmdUnknown(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) => connectionEntry.LastCommand = this;

    public string? Execute() {
        Logger.Log.Information($"Received an unknown command from a throttle: '{ConnectionEntry.ConnectionID}' of '{CmdString}'");
        return null;
    }

    public override string ToString() => "COMMAND: UNKNOWN";
}