using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

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

    public override string ToString() => $"MSG:MSGAddress=>{DisplayTerminators(Message)}";
}