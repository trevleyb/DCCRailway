using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgQuit(ILogger logger) : IClientMsg {
    public void Process(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd Quit - {0}:=>'{1}'", ToString(), commandStr);
    }

    public override string ToString() {
        return "MSG:Quit";
    }
}