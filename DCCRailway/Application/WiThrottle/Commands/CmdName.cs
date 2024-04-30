using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdName(WiThrottleConnection connection, WiThrottleServerOptions options)
    : ThrottleCmd(connection, options), IThrottleCmd {

    // Recieved a "NAME" [N] command from a Client and so we need to process it.
    // ------------------------------------------------------------------------
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        if (commandStr.Length > 1) {
            var deviceName = commandStr[1..].Replace("???", "'");
            Connection.ThrottleName = deviceName;
            Logger.Log.Debug("CmdFactory [{0}]: Set the connection device name to '{1}'",Connection.ConnectionID, deviceName);
            Connection.AddResponseMsg(new MsgServerID(Connection,Options));
            Connection.AddResponseMsg(new MsgRosterList(Connection, Options));
            Connection.AddResponseMsg(new MsgTurnoutLabels(Connection, Options));
            Connection.AddResponseMsg(new MsgTurnoutList(Connection, Options));
            Connection.AddResponseMsg(new MsgRouteLabels(Connection, Options));
            Connection.AddResponseMsg(new MsgRouteList(Connection, Options));
            Connection.AddResponseMsg(new MsgHeartbeat(Connection,Options));
        }
    }
    public override string ToString() => "CMD:Name";
}