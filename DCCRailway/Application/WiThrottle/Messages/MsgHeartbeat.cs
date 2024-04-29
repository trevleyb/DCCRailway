using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgHeartbeat(WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleMsg(connection, options), IThrottleMsg {
    public string Message => Terminators.AddTerminator($"*{Connection.HeartbeatSeconds:D2}");
    public override string ToString() => $"MSG:Heartbeat=>{NoTerminators(Message)}";

}