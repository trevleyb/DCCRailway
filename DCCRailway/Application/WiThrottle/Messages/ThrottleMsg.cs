using DCCRailway.Application.WiThrottle.Helpers;

namespace DCCRailway.Application.WiThrottle.Messages;

public abstract class ThrottleMsg : IThrottleMsg {
    public bool IsValid => !string.IsNullOrEmpty(Terminators.RemoveTerminators(Message)) && Terminators.HasTerminator(Message);
    protected string DisplayTerminators(string message) => message.Replace((char)0x0A, '•').Replace((char)0x0d, '•');
    public abstract string Message { get; }
}