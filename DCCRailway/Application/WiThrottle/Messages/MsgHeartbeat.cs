using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgHeartbeat(WiThrottleServerOptions options) : ThrottleMsg, IThrottleMsg {
    public string Message => Terminators.AddTerminator($"*{options.HeartbeatSeconds:D2}");
    public override string ToString() => $"MSG:Heartbeat=>{NoTerminators(Message)}";

}