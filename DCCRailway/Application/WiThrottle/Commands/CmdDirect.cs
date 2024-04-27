namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdDirect : ThrottleCmdBase, IThrottleCmd {
    public CmdDirect(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) => connectionEntry.LastCommand = this;

    public string? Execute() => null;

    public override string ToString() => "COMMAND: SEND DIRECT DATA";
}