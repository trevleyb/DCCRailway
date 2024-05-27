using System.Text;
using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Messages;

public class MsgFunctionState(Connection connection, MultiThrottleMessage data, byte function, FunctionStateEnum stateEnum) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.Append($"M{data.Group}{data.Function}");
            sb.Append(data.Address.IsLong ? "L" : "S");
            sb.Append(data.Address.Address);
            sb.Append("<;>");
            sb.AppendLine($"F{stateEnum.ToString()}{function}");
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:MSGFunctionState [{connection?.ToString() ?? ""}]";
    }
}