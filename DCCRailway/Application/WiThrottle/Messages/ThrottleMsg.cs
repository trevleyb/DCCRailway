using DCCRailway.Application.WiThrottle.Helpers;

namespace DCCRailway.Application.WiThrottle.Messages;

public abstract class ThrottleMsg : IThrottleMsg {
    public bool IsValid => !string.IsNullOrEmpty(Terminators.RemoveTerminators(Message)) && Terminators.HasTerminator(Message);
    public abstract string Message { get; }
}