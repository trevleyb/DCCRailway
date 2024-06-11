using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgHeartbeat(ILogger logger) : IClientMsg {
    public int HeartbeatSeconds { get; private set; }

    public void Process(string commandStr) {
        logger.Debug("WiThrottle Recieved Cmd Heartbeat - {0}:=>'{1}'", ToString(), commandStr);
        if (commandStr.Length == 0) return;
        if (commandStr[0] == '*' || commandStr[0] == '#') {
            if (int.TryParse(commandStr.Substring(1), out var secs)) {
                HeartbeatSeconds = secs;
            }
        }
    }

    public override string ToString() {
        return "MSG:Heartbeat";
    }
}