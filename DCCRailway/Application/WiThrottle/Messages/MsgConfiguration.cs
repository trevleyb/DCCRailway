using System.Text;
using DCCRailway.Application.WiThrottle.Commands;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgConfiguration(WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleMsg(connection, options), IThrottleMsg {
    public string Message {
        get {
            var sb = new StringBuilder();
            sb.AppendLine ("VN2.0");
            sb.AppendLine ("RL0");  // TODO: Add rester entries
            sb.AppendLine ("PPA0"); // TODO: Add Power Suppoert
            //response.AppendLine ("PTT0");		// TODO: Add Turnouts
            //response.AppendLine ("PRT0");		// TODO: Add Routes
            //response.AppendLine ("PCC0");		// TODO: Add Consists
            sb.AppendLine ("PW12080");
            sb.AppendLine(new MsgHeartbeat(Connection, Options).Message);
            return sb.ToString();
        }
    }
    public override string ToString() => $"MSG:Configuration=>{NoTerminators(Message)}";
}