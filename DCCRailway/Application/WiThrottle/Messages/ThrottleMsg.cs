namespace DCCRailway.Application.WiThrottle.Messages;

public abstract class ThrottleMsg(WiThrottleConnection connection, WiThrottleServerOptions options) {

    protected readonly WiThrottleConnection    Connection = connection;
    protected          WiThrottleServerOptions Options = options;

    protected string NoTerminators(string message) => message.Replace((char)0x0A, '•').Replace((char)0x0d, '•');

}