using System.Text;
using DCCRailway.Common.Types;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgAddress(Connection connection, MultiThrottleMessage data, DCCAddress address)
    : ThrottleMsg, IThrottleMsg
{
    public MsgAddress(Connection connection, MultiThrottleMessage data) : this(connection, data, data.Address)
    {
    }

    public override string Message
    {
        get
        {
            var sb = new StringBuilder();
            sb.Append($"M{data.Group}{data.Function}");
            sb.Append(address.IsLong ? "L" : "S");
            sb.Append(address.Address);
            sb.AppendLine("<;>");
            return sb.ToString();
        }
    }

    public override string ToString() => $"MSG:MSGAddress [{connection?.ToString() ?? ""}]";
}