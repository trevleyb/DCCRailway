using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdHeartBeat : ThrottleCmd, IThrottleCmd {
    public CmdHeartBeat(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) => connectionEntry.LastCommand = this;

    public string? Execute() {
        switch (CmdString) {
        case "+":
            Logger.Log.Information($"Received a HEARTBEAT + (ON) command from '{ConnectionEntry.ThrottleName}'");
            ConnectionEntry.HeartbeatState = HeartbeatStateEnum.On;
            break;
        case "-":
            Logger.Log.Information($"Received a HEARTBEAT - (OFF) command from '{ConnectionEntry.ThrottleName}'");
            ConnectionEntry.HeartbeatState = HeartbeatStateEnum.Off;
            break;
        default:
            Logger.Log.Information($"Received a HEARTBEAT from '{ConnectionEntry.ThrottleName}'");
            break;
        }
        return null;
    }

    public override string ToString() => "COMMAND: HEARTBEAT";
}