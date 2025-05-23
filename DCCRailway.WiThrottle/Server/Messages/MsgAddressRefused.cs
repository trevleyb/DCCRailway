using System.Text;
using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Server.Messages;

public class MsgAddressRefused(Connection connection, DCCAddress address, char group) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.Append($"M{group}S");
            sb.Append(address.IsLong ? "L" : "S");
            sb.Append(address.Address);
            sb.AppendLine("<;>");
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:MSGAddressRefused [{connection?.ToString() ?? ""}]";
    }
}