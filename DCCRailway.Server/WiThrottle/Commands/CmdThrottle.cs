namespace DCCRailway.Server.WiThrottle.Commands; 

public class CmdThrottle : ThrottleCmdBase, IThrottleCmd {
    public CmdThrottle(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) {
        connectionEntry.LastCommand = this;
    }

    public string? Execute() {
        return null;
    }

    public override string ToString() {
        return "COMMAND: THROTTLE COMMAND";
    }
}