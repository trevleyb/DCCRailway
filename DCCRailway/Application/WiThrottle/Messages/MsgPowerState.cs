using System.Text;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgPowerState(WiThrottleServerOptions options) : ThrottleMsg, IThrottleMsg {
    public string Message {
        get {
            var sb = new StringBuilder();
            var powerStateMsg = GetPowerStateMsg();
            if (!string.IsNullOrEmpty(powerStateMsg)) sb.AppendLine(powerStateMsg);
            return sb.ToString();
        }
    }

    private string? GetPowerStateMsg() {
        try {
            if (options.Controller != null && options.Controller.IsCommandSupported<ICmdPowerGetState>()) {
                var powerCmd = options.Controller.CreateCommand<ICmdPowerGetState>();
                if (powerCmd != null) {
                    var powerState = options.Controller.Execute(powerCmd) as IResultPowerState;
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

    public override string ToString() => $"MSG:PowerState=>{NoTerminators(Message)}";
}