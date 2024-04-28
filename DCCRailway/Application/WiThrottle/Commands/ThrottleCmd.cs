namespace DCCRailway.Application.WiThrottle.Commands;

public abstract class ThrottleCmd {

    protected readonly string                    CmdString;
    protected readonly WiThrottleConnectionEntry ConnectionEntry;
    protected WiThrottleServerOptions            Options;

    protected ThrottleCmd(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) {
        Options         = options;
        ConnectionEntry = connectionEntry;
        CmdString       = cmdString;
        ConnectionEntry.UpdateHeartbeat();
    }
}