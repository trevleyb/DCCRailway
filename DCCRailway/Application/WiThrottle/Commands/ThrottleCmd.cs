namespace DCCRailway.Application.WiThrottle.Commands;

public abstract class ThrottleCmd {

    protected readonly string               CmdString;
    protected readonly WiThrottleConnection Connection;
    protected WiThrottleServerOptions       Options;

    protected ThrottleCmd(WiThrottleConnection connection, string cmdString, ref WiThrottleServerOptions options) {
        Options    = options;
        Connection = connection;
        CmdString  = cmdString;
        Connection.UpdateHeartbeat();
    }
}