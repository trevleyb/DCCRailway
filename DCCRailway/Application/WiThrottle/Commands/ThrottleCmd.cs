namespace DCCRailway.Application.WiThrottle.Commands;

public abstract class ThrottleCmd {

    protected readonly WiThrottleConnection Connection;
    protected WiThrottleServerOptions       Options;

    protected ThrottleCmd(WiThrottleConnection connection, WiThrottleServerOptions options) {
        Options    = options;
        Connection = connection;
        Connection.UpdateHeartbeat();
    }
}