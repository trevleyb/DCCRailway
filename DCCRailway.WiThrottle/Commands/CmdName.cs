using DCCRailway.WiThrottle.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdName(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd {
    // Recieved a "NAME" [N] command from a Client and so we need to process it.
    // ------------------------------------------------------------------------
    public void Execute(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd from [{0}]: Name - {1}=>'{2}'", connection.ConnectionHandle, ToString(), commandStr);

        if (commandStr.Length > 1) {
            var deviceName = commandStr[1..].Replace("???", "'");
            connection.ThrottleName = deviceName;
            connection.QueueMsg(new MsgServerID(connection));
            connection.QueueMsg(new MsgHeartbeat(connection));
            connection.QueueMsg(new MsgPowerState(connection));
            connection.QueueMsg(new MsgRosterList(connection));
            connection.QueueMsg(new MsgTurnoutLabels(connection));
            connection.QueueMsg(new MsgTurnoutList(connection));
            connection.QueueMsg(new MsgRouteLabels(connection));
            connection.QueueMsg(new MsgRouteList(connection));
            connection.QueueMsg(new MsgFastClock(connection.RailwaySettings));
        }
    }

    public override string ToString() {
        return "CMD:Name";
    }
}