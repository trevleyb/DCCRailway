using DCCRailway.Railway.Throttles.WiThrottle.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle.Messages;

public class MsgHeartbeat(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message    => Terminators.AddTerminator($"*{connection.HeartbeatSeconds:D2}");
    public override string ToString() => $"MSG:Heartbeat [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}