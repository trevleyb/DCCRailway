using DCCRailway.WiThrottle.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdName(ILogger logger, WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    // Recieved a "NAME" [N] command from a Client and so we need to process it.
    // ------------------------------------------------------------------------
    public void Execute(string commandStr) {
        logger.ForContext<WiThrottleServer>().Information("{0}=>'{1}'", ToString(), commandStr);
        if (commandStr.Length > 1) {
            var deviceName = commandStr[1..].Replace("???", "'");
            connection.ThrottleName = deviceName;
            logger.ForContext<WiThrottleServer>().Verbose("{0}:{2}=> Set the connection device name to '{1}'", connection.ConnectionHandle, deviceName, connection.ToString());

            connection.QueueMsg(new MsgServerID(connection));
            connection.QueueMsg(new MsgHeartbeat(connection));
            connection.QueueMsg(new MsgPowerState(connection));
            connection.QueueMsg(new MsgRosterList(connection));
            connection.QueueMsg(new MsgTurnoutLabels(connection));
            connection.QueueMsg(new MsgTurnoutList(connection));
            connection.QueueMsg(new MsgRouteLabels(connection));
            connection.QueueMsg(new MsgRouteList(connection));
            connection.QueueMsg(new MsgHeartbeat(connection));
        }
    }

    public override string ToString() => "CMD:Name";
}