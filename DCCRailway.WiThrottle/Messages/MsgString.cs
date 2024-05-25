namespace DCCRailway.WiThrottle.Messages;

public class MsgString(string message) : ThrottleMsg, IThrottleMsg {
    public override string Message => message;

    public override string ToString() {
        return $"MSG:StringOnly [{message}]";
    }
}