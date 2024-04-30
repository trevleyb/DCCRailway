using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdName(WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd, IThrottleCmd {

    // Recieved a "NAME" [N] command from a Client and so we need to process it.
    // ------------------------------------------------------------------------
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        if (commandStr.Length > 1) {
            var deviceName = commandStr[1..].Replace("???", "'");
            connection.ThrottleName = deviceName;
            Logger.Log.Debug("CmdFactory [{0}]: Set the connection device name to '{1}'",connection.ConnectionID, deviceName);
            connection.AddResponseMsg(new MsgServerID(options));
            connection.AddResponseMsg(new MsgPowerState(options));
            connection.AddResponseMsg(new MsgRosterList(options));
            connection.AddResponseMsg(new MsgTurnoutLabels(options));
            connection.AddResponseMsg(new MsgTurnoutList(options));
            connection.AddResponseMsg(new MsgRouteLabels(options));
            connection.AddResponseMsg(new MsgRouteList(options));
            connection.AddResponseMsg(new MsgHeartbeat(options));
        }
    }
    public override string ToString() => "CMD:Name";
}