using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgPowerState(WiThrottleConnection connection) : ThrottleMsg, IThrottleMsg {
    public override string Message {
        get {
            var sb = new StringBuilder();
            var powerStateMsg = GetPowerStateMsg();
            if (!string.IsNullOrEmpty(powerStateMsg)) sb.AppendLine(powerStateMsg);
            return sb.ToString();
        }
    }

    private string? GetPowerStateMsg() {
        try {
            var layoutCmd = new WitThrottleLayoutCmd(connection.ActiveController);
            var powerMsg = layoutCmd.PowerState switch {
                DCCPowerState.On      => "PPA1",
                DCCPowerState.Off     => "PPA0",
                DCCPowerState.Unknown => "PPA2",
                _                     => null
            };
            return powerMsg;
        }
        catch {
            return null;
        }
    }

    public override string ToString() => $"MSG:PowerState [{connection?.ToString() ?? ""}]=>{Terminators.ForDisplay(Message)}";
}