using System.Text;
using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Server.Messages;

public class MsgFunctionState(Connection connection, DCCAddress address, char group, char function, byte functionNum, FunctionStateEnum stateEnum) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.Append($"M{group}{function}");
            sb.Append(address.IsLong ? "L" : "S");
            sb.Append(address.Address);
            sb.Append("<;>");
            sb.AppendLine($"F{(stateEnum == FunctionStateEnum.On ? "1" : "0")}{functionNum}");
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:MSGFunctionState [{connection?.ToString() ?? ""}]";
    }
}