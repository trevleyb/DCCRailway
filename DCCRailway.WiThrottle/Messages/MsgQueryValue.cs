using System.Text;
using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Messages;

public class MsgQueryValue(Connection connection, DCCAddress address, char group, char function, char queryType, string queryValue) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.Append($"M{group}{function}");
            sb.Append(address.IsLong ? "L" : "S");
            sb.Append(address.Address);
            sb.Append("<;>");
            sb.AppendLine($"{queryType}{queryValue}");
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:MSGFunctionState [{connection?.ToString() ?? ""}]";
    }
}