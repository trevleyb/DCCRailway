namespace DCCRailway.Server.WiThrottle.Commands;

public abstract class ThrottleCmdBase {
    protected readonly string                    CmdString;
    protected readonly WiThrottleConnectionEntry ConnectionEntry;

    protected ThrottleCmdBase(WiThrottleConnectionEntry connectionEntry, string cmdString) {
        ConnectionEntry = connectionEntry;
        CmdString       = cmdString;
        ConnectionEntry.UpdateHeartbeat();
    }
}