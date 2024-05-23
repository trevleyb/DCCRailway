using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgHeartbeat(Connection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message => Terminators.AddTerminator($"*{connection.HeartbeatSeconds:D2}");

    public override string ToString() {
        return $"MSG:Heartbeat [{connection?.ToString() ?? ""}]";
    }
}