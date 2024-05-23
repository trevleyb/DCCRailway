using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgServerID(Connection connection) : ThrottleMsg, IThrottleMsg
{
    public override string Message    => Terminators.AddTerminator("VN2.0");
    public override string ToString() => $"MSG:ServerID [{connection?.ToString() ?? ""}]";
}