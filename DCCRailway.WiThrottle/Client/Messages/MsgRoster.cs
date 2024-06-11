using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgRoster(ILogger logger) : IClientMsg {
    public void Process(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd Roster - {0}=>'{1}'", ToString(), commandStr);
    }

    public override string ToString() {
        return "MSG:Roster";
    }
}