namespace DCCRailway.Application.WiThrottle.Messages;

public abstract class ThrottleMsg {
    protected string NoTerminators(string message) => message.Replace((char)0x0A, '•').Replace((char)0x0d, '•');
}