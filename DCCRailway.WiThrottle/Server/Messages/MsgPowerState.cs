using System.Text;
using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Server.Messages;

public class MsgPowerState(Connection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb            = new StringBuilder();
            var powerStateMsg = GetPowerStateMsg();
            if (!string.IsNullOrEmpty(powerStateMsg)) sb.AppendLine(powerStateMsg);
            return sb.ToString();
        }
    }

    private string? GetPowerStateMsg() {
        try {
            var layoutCmd = new LayoutCmdHelper(connection.CommandStation);

            var powerMsg = layoutCmd.PowerState switch {
                DCCPowerState.On      => "PPA1",
                DCCPowerState.Off     => "PPA0",
                DCCPowerState.Unknown => "PPA2",
                _                     => null
            };

            return powerMsg;
        } catch {
            return null;
        }
    }

    public override string ToString() {
        return $"MSG:PowerState [{connection?.ToString() ?? ""}]";
    }
}