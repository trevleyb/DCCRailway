using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgHeartbeat(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message => Terminators.AddTerminator($"*{connection.HeartbeatSeconds:D2}");
    public override string ToString() => $"MSG:Heartbeat [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";

}