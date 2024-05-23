using System.Text;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgAddressRefused(Connection connection, MultiThrottleMessage data) : ThrottleMsg, IThrottleMsg
{
    public override string Message
    {
        get
        {
            var sb = new StringBuilder();
            sb.Append($"M{data.Group}S");
            sb.Append(data.Address.IsLong ? "L" : "S");
            sb.Append(data.Address.Address);
            sb.AppendLine("<;>");
            return sb.ToString();
        }
    }

    public override string ToString() => $"MSG:MSGAddressRefused [{connection?.ToString() ?? ""}]";
}