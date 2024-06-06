using System.Text;
using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Messages;

public class MsgAddressReleased(Connection connection, DCCAddress address, char group, char function) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.Append($"M{group}-");
            sb.Append(address.IsLong ? "L" : "S");
            sb.Append(address.Address);
            sb.AppendLine("<;>");
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:MSGAddressReleased [{connection?.ToString() ?? ""}]";
    }
}