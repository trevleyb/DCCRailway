using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public abstract class ThrottleMsg : IThrottleMsg {
    public abstract string Message { get; }
}