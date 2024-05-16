using DCCRailway.Common.Helpers;
using DCCRailway.Railway.Throttles.WiThrottle.Messages;
using Serilog;

namespace DCCRailway.Railway.Throttles.WiThrottle.Commands;

public class CmdHardware(ILogger logger, WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    // When we get a HardwareID, we need to see if we already have one in our current list of
    // connections. This is because there might have been a temporary disconnect and now a
    // reconnect of the throttle. So we want to re-use the data that we prefiously had.
    // ---------------------------------------------------------------------------------------

    public void Execute(string commandStr) {
        logger.ForContext<WiThrottleServer>().Information("{0}=>'{1}'", ToString(), commandStr);
        if (commandStr.Length > 2)
            switch (commandStr[1]) {
            case 'U':
                var hardwareID = commandStr[2..];
                connection.HardwareID = hardwareID;
                if (connection.HasDuplicateID(hardwareID)) {
                    logger.ForContext<WiThrottleServer>().Debug("{0}:{1} Duplicate HardwareIDs found - re-using previous connection.", ToString(), connection.ToString());

                    // Get the other connection (first one that has the same hardwareID but a different connectionID)
                    // ----------------------------------------------------------------------------------------------
                    var newConnection = connection.GetByHardwareID(hardwareID);
                    if (newConnection is not null) {
                        var client = connection.Client;
                        connection = newConnection;
                        connection.RemoveDuplicateID(hardwareID);
                        connection.Client = client;
                    }
                }
                logger.ForContext<WiThrottleServer>().Debug("{0}:{2} Set the hardwareID to '{1}'", ToString(), hardwareID, connection.ToString());
                connection.QueueMsg(new MsgHardware(connection));
                break;
            }
    }

    public override string ToString() => "CMD:Hardware";
}