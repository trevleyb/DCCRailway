using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdPanel (WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd, IThrottleCmd {
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
        var turnout = options.Config?.Turnouts[turnoutID];
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
        connection.AddResponseMsg(new MsgTurnoutState(options,turnout));
    }

    /// <summary>
    /// Active a Route. You cannot inactivate a route, so the only
    /// option is command 2 - activate it.
    /// </summary>
    private void SetRoute(string commandStr) {
        var routeID = commandStr[1..];
        var route = options.Config?.Routes[routeID];
        if (route != null) {
            switch (commandStr[0]) {
            case '2':
                break;
            }
        }
        connection.AddResponseMsg(new MsgRouteState(options,route));
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
            if (options.Controller != null && options.Controller.IsCommandSupported<ICmdPowerSetOff>()) {
                var powerCmd = options.Controller.CreateCommand<ICmdPowerSetOff>();
                if (powerCmd != null) options.Controller.Execute(powerCmd);
            }
            break;
        case '1':
            if (options.Controller != null && options.Controller.IsCommandSupported<ICmdPowerSetOn>()) {
                var powerCmd = options.Controller.CreateCommand<ICmdPowerSetOn>();
                if (powerCmd != null) options.Controller.Execute(powerCmd);
            }
            break;
        }
        connection.AddResponseMsg(new MsgPowerState(options));
    }

    public override string ToString() => "CMD:Panel";
}