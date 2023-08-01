namespace DCCRailway.Server.WiThrottle.Commands;

public class CmdDirect : ThrottleCmdBase, IThrottleCmd {
    public CmdDirect(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
        connectionEntry.LastCommand = this;
    }

    public string? Execute() {
        return null;
    }

    public override string ToString() {
        return "COMMAND: SEND DIRECT DATA";
    }
}