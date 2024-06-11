using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgIgnore(ILogger logger) : IClientMsg {
    public void Process(string commandStr) {
        logger.Information("WiThrottle Client Ignore");
    }

    public override string ToString() {
        return "MSG:Ignore";
    }
}