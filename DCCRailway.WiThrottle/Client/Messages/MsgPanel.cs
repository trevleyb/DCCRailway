using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgPanel(ILogger logger) : IClientMsg {
    public void Process(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd Panel {0}:=>'{1}'", ToString(), commandStr);
    }

    public override string ToString() {
        return "MSG:Panel";
    }
}