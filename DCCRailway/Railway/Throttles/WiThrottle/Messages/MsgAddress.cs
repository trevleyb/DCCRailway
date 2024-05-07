using System.Text;
using DCCRailway.Railway.Throttles.WiThrottle.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle.Messages;

public class MsgAddress(WiThrottleConnection connection, MultiThrottleMessage data ) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.Append($"M{data.Group}{data.Function}");
            sb.Append(data.Address.IsLong ? "L" : "S");
            sb.Append(data.Address.Address);
            sb.AppendLine("<;>");
            return sb.ToString();
        }
    }

    public override string ToString() => $"MSG:MSGAddress [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}