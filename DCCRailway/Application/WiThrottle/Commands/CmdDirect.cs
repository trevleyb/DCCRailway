namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdDirect : ThrottleCmd, IThrottleCmd {
    public CmdDirect(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;

    public string? Execute() => null;

    public override string ToString() => "COMMAND: SEND DIRECT DATA";
}