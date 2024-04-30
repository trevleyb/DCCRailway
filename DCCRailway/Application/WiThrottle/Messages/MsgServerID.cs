using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgServerID(WiThrottleServerOptions options) : ThrottleMsg, IThrottleMsg {
    public string Message => Terminators.AddTerminator("VN2.0");
    public override string ToString() => $"MSG:ServerID=>{NoTerminators(Message)}";

}