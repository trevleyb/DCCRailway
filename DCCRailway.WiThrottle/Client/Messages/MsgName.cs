using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgName(ILogger logger) : IClientMsg {
    public void Process(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd Name - {0}=>'{1}'", ToString(), commandStr);
    }

    public override string ToString() {
        return "MSG:Name";
    }
}