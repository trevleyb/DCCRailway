using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgConfiguration(WiThrottleServerOptions options) : ThrottleMsg, IThrottleMsg {
    public string Message {
        get {
            var sb = new StringBuilder();
            sb.AppendLine ("VN2.0"); // Support for version 2.0 of the Protocol
            //sb.AppendLine ($"PW{options.Port}"); // Only send this once WebServer is running
            return sb.ToString();
        }
    }

    public override string ToString() => $"MSG:Configuration=>{NoTerminators(Message)}";
}