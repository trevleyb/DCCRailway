namespace DCCRailway.WiThrottle.Server.Messages;

public class MsgIgnore : ThrottleMsg, IThrottleMsg {
    public override string Message => "";

    public override string ToString() {
        return $"MSG:Ignore";
    }
}