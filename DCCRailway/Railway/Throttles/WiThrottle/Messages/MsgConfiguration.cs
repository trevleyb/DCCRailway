using System.Text;
using DCCRailway.Railway.Throttles.WiThrottle.Helpers;

namespace DCCRailway.Railway.Throttles.WiThrottle.Messages;

public class MsgConfiguration(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.AppendLine("VN2.0"); // Support for version 2.0 of the Protocol

            //sb.AppendLine ($"PW{options.Port}"); // Only send this once WebServer is running
            return sb.ToString();
        }
    }

    public override string ToString() => $"MSG:Configuration [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}