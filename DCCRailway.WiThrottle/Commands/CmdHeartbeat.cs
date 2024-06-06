using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdHeartbeat(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.Debug("WiThrottle Recieved Cmd from [{0}]: Heartbeat - {1}:{3}=>'{2}'", connection.ConnectionHandle, ToString(), commandStr, connection.ToString());
        if (commandStr.Length == 0) return;

        if (commandStr[0] == '*') {
            if (commandStr.Length == 1) {
                connection.UpdateHeartbeat();
            } else if (commandStr.Length >= 2) {
                if (commandStr[1] == '+') connection.HeartbeatState      = HeartbeatStateEnum.On;
                else if (commandStr[1] == '-') connection.HeartbeatState = HeartbeatStateEnum.Off;
                else {
                    // We actually should never get here. It should not be possible for a client to change the 
                    // duration of the heartbeats. But lets check in case some do. 
                    if (int.TryParse(commandStr[1..], out var heartBeatSeconds)) {
                        logger.Verbose("WiThrottle Recieved a new HeartBeat duration from {0}: Heartbeat set to {1}", connection.ConnectionHandle, heartBeatSeconds);
                        connection.HeartbeatSeconds = heartBeatSeconds;
                    } else {
                        logger.Verbose("WiThrottle Recieved Cmd from {0}: Heartbeat - {1}:{2}=>Heartbeat Receievd'", connection.ConnectionHandle, ToString(), commandStr);
                    }
                }
            }
        }
    }

    public override string ToString() {
        return "CMD:Heartbeat";
    }
}