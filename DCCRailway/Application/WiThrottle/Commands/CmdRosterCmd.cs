namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdRosterCmd : ThrottleCmd, IThrottleCmd {
    public CmdRosterCmd(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;

    public string? Execute() => null;

    public override string ToString() => "COMMAND: ROSTER COMMAND";
}