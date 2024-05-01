using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdPanel (WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {

        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        try {
            var cmd = commandStr[0..2];
            switch (cmd.ToUpper()) {
            case "PPA":     // Set the Power
                SetPowerState(commandStr[3]);
                break;
            case "PTA":     // Set a Turnout
                ThrowTurnout(commandStr[3..]);
                break;
            case "PRA":     // Set a Route
                SetRoute(commandStr[3..]);
                break;
            case "PFT":
                Logger.Log.Information("{0}: Fast Clock not currently supported=>'{1}'",ToString(),commandStr);
                break;
            default:
                Logger.Log.Information("{0}: Unknown Panel Command recieved=>'{1}'",ToString(),commandStr);
                break;
            }
        }
        catch {
            Logger.Log.Error("{0}: Unable to Process the command =>'{1}'",ToString(),commandStr);
        }
    }


    /// <summary>
    /// See if we can find the turnout ID provided and if we can, then
    /// throw to switch it and finally send a message to tell the throttle
    /// of its current state.
    /// </summary>
    private void ThrowTurnout(string commandStr) {
        var turnoutID = commandStr[1..];
        var turnout = connection.RailwayConfig.Turnouts?[turnoutID];
        if (turnout != null) {
            switch (commandStr[0]) {
            case '2':
                break;
            case 'C':
                break;
            case 'T':
                break;
            }
        }
        connection.QueueMsg(new MsgTurnoutState(connection,turnout));
    }

    /// <summary>
    /// Active a Route. You cannot inactivate a route, so the only
    /// option is command 2 - activate it.
    /// </summary>
    private void SetRoute(string commandStr) {
        var routeID = commandStr[1..];
        var route = connection.RailwayConfig.Routes?[routeID];
        if (route != null) {
            switch (commandStr[0]) {
            case '2':
                break;
            }
        }
        connection.QueueMsg(new MsgRouteState(connection,route));
    }

    /// <summary>
    /// Looks at the 4th charater of the message and uses it to
    /// turn on or off the Power to the Layout. It then sends a PowerMsg
    /// update back to the client.
    /// </summary>
    /// <param name="state"></param>
    private void SetPowerState(char state) {

        switch (state) {
        case '0':
            if (connection.ActiveController.IsCommandSupported<ICmdPowerSetOff>()) {
                var powerCmd = connection.ActiveController.CreateCommand<ICmdPowerSetOff>();
                if (powerCmd != null) connection.ActiveController.Execute(powerCmd);
            }
            break;
        case '1':
            if (connection.ActiveController.IsCommandSupported<ICmdPowerSetOn>()) {
                var powerCmd = connection.ActiveController.CreateCommand<ICmdPowerSetOn>();
                if (powerCmd != null) connection.ActiveController.Execute(powerCmd);
            }
            break;
        }
        connection.QueueMsg(new MsgPowerState(connection));
    }

    public override string ToString() => "CMD:Panel";
}