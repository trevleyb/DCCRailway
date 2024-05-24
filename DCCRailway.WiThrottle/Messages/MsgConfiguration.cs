using System.Text;

namespace DCCRailway.WiThrottle.Messages;

public class MsgConfiguration(Connection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            sb.AppendLine("VN2.0"); // Support for version 2.0 of the Protocol

            //sb.AppendLine ($"PW{options.Port}"); // Only send this once WebServer is running
            return sb.ToString();
        }
    }

    public override string ToString() {
        return $"MSG:Configuration [{connection?.ToString() ?? ""}]";
    }
}