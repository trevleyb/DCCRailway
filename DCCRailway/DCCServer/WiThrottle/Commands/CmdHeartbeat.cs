using DCCRailway.Common.Utilities;

namespace DCCRailway.DCCServer.WiThrottle.Commands;

public class CmdHeartBeat : ThrottleCmdBase, IThrottleCmd {
    public CmdHeartBeat(WiThrottleConnectionEntry connectionEntry, string cmdString) : base(connectionEntry, cmdString) => connectionEntry.LastCommand = this;

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