using DCCRailway.WiThrottle.Server.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Server.Commands;

public class CmdHardware(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd {
    // When we get a HardwareID, we need to see if we already have one in our current list of
    // connections. This is because there might have been a temporary disconnect and now a
    // reconnect of the throttle. So we want to re-use the data that we prefiously had.
    // ---------------------------------------------------------------------------------------

    public void Execute(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd from [{0}]: Hardware - {1}=>'{2}'", connection.ConnectionHandle, ToString(), commandStr);

        if (commandStr.Length > 2) {
            switch (commandStr[1]) {
            case 'U':
                var hardwareID = commandStr[2..];
                connection.HardwareID = hardwareID;

                if (connection.HasDuplicateID(hardwareID)) {
                    logger.Information("WiThrottle Duplicate HardwareIDs found {0}:{1} - re-using previous connection.", ToString(), connection.ToString());
                    connection.MergePreviousConenctionData();
                } else {
                    logger.Information("WiThrottle New Throttle added {0}.", connection.ToString());
                    connection.QueueMsg(new MsgHardware(connection));
                    connection.QueueMsg(new MsgHeartbeat(connection));
                }

                break;
            }
        }
    }

    public override string ToString() {
        return "CMD:Hardware";
    }
}