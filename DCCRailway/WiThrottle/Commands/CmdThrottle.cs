namespace DCCRailway.Station.WiThrottle.Commands;

public class CmdThrottle : ThrottleCmdBase, IThrottleCmd {
    public CmdThrottle(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) => connectionEntry.LastCommand = this;
    public string? Execute() => null;
    public override string ToString() => "COMMAND: THROTTLE COMMAND";
}