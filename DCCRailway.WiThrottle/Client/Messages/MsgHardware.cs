using Serilog;

namespace DCCRailway.WiThrottle.Client.Messages;

public class MsgHardware(ILogger logger) : IClientMsg {
    // When we get a HardwareID, we need to see if we already have one in our current list of
    // connections. This is because there might have been a temporary disconnect and now a
    // reconnect of the throttle. So we want to re-use the data that we prefiously had.
    // ---------------------------------------------------------------------------------------

    public void Process(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd Hardware - {0}=>'{1}'", ToString(), commandStr);

        if (commandStr.Length > 2)
            switch (commandStr[1]) {
            case 'U':
                var hardwareID = commandStr[2..];
                break;
            }
    }

    public override string ToString() {
        return "MSG:Hardware";
    }
}