using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgServerID(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message    => Terminators.AddTerminator("VN2.0");
    public override string ToString() => $"MSG:ServerID [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}