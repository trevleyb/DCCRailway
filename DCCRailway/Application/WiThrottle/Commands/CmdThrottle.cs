namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdThrottle : ThrottleCmd, IThrottleCmd {
    public CmdThrottle(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;
    public string? Execute() => null;
    public override string ToString() => "COMMAND: THROTTLE COMMAND";
}