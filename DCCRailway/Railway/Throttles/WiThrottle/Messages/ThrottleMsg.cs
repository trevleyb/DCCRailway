using DCCRailway.Railway.Throttles.WiThrottle.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle.Messages;

public abstract class ThrottleMsg : IThrottleMsg {
    public bool IsValid => !string.IsNullOrEmpty(Terminators.RemoveTerminators(Message)) && Terminators.HasTerminator(Message);
    public abstract string Message { get; }
}