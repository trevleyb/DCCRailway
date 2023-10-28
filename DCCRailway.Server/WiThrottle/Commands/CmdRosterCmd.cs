namespace DCCRailway.Server.WiThrottle.Commands;

public class CmdRosterCmd : ThrottleCmdBase, IThrottleCmd {
    public CmdRosterCmd(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) => connectionEntry.LastCommand = this;

    public string? Execute() => null;

    public override string ToString() => "COMMAND: ROSTER COMMAND";
}