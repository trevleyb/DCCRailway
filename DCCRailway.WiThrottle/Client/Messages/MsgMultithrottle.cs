using DCCRailway.WiThrottle.Client.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Server.Commands;

public class MsgMultiThrottle(ILogger logger) : IClientMsg {
    public void Process(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd Multithrottle - {0}:=>'{1}'", ToString(), commandStr);
        var data = new MsgMultiThrottleHelper(commandStr);
    }
}