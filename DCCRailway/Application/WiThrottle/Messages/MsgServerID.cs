using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgServerID(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message => Terminators.AddTerminator("VN2.0");
    public override string ToString() => $"MSG:ServerID=>{DisplayTerminators(Message)}";

}