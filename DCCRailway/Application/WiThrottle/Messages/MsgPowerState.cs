using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
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
            if (connection.ActiveController.IsCommandSupported<ICmdPowerGetState>()) {
                var powerCmd = connection.ActiveController.CreateCommand<ICmdPowerGetState>();
                if (powerCmd != null) {
                    var powerState = connection.ActiveController.Execute(powerCmd) as IResultPowerState;
                    var powerMsg = powerState?.State switch {
                        DCCPowerState.On      => "PPA1",
                        DCCPowerState.Off     => "PPA0",
                        DCCPowerState.Unknown => "PPA2",
                        _                     => null
                    };
                    if (!string.IsNullOrEmpty(powerMsg)) return powerMsg;
                }
            }
            return null;
        }
        catch {
            return null;
        }
    }

    public override string ToString() => $"MSG:PowerState [{connection?.ConnectionID ?? 0}]=>{DisplayTerminators(Message)}";
}