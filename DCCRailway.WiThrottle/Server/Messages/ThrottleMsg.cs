namespace DCCRailway.WiThrottle.Server.Messages;

public abstract class ThrottleMsg : IThrottleMsg {
    public abstract string Message { get; }
}