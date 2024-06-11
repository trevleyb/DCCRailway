using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Server.Messages;

public class MsgServerID(Connection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message => Terminators.AddTerminator("VN2.0");

    public override string ToString() {
        return $"MSG:ServerID [{connection?.ToString() ?? ""}]";
    }
}