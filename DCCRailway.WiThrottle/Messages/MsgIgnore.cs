namespace DCCRailway.WiThrottle.Messages;

public class MsgIgnore : ThrottleMsg, IThrottleMsg {
    public override string Message => "";
    public override string ToString() => $"MSG:Ignore";
}