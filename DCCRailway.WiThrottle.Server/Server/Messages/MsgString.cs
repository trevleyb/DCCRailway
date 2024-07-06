using DCCRailway.WiThrottle.Helpers;

namespace DCCRailway.WiThrottle.Server.Messages;

public class MsgString(string message) : ThrottleMsg, IThrottleMsg {
    public override string Message => Terminators.AddTerminator(message);

    public override string ToString() {
        return $"MSG:StringOnly [{message}]";
    }
}