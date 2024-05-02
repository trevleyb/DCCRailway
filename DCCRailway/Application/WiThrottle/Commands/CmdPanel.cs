using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdPanel (WiThrottleConnection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {

        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        try {
            switch (commandStr[0..3].ToUpper()) {
                case "PPA": SetPowerState(commandStr[3]); break;
                case "PTA": ThrowTurnout(commandStr[3..]);break;
                case "PRA": SetRoute(commandStr[3..]);break;
                case "PFT": Logger.Log.Information("{0}: Fast Clock not currently supported=>'{1}'", ToString(), commandStr); break;
                default: Logger.Log.Information("{0}: Unknown Panel TurnoutCommand recieved=>'{1}'", ToString(), commandStr); break;
            };
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
    private async void ThrowTurnout(string commandStr) {
        var turnoutID = commandStr[1..];
        if (connection.RailwayConfig.Turnouts is { } turnouts) {
            var turnout = await turnouts.GetByIDAsync(turnoutID);
            var layoutCmds = new WitThrottleLayoutCmd(connection.ActiveController, turnout?.Address);

            if (turnout != null) {
                turnout.CurrentState = (TurnoutCommand)commandStr[0] switch {
                    TurnoutCommand.ToggleTurnout => turnout.CurrentState == DCCTurnoutState.Closed ? DCCTurnoutState.Thrown : DCCTurnoutState.Closed,
                    TurnoutCommand.CloseTurnout  => DCCTurnoutState.Closed,
                    TurnoutCommand.ThrowTurnout  => DCCTurnoutState.Thrown,
                    _                            => throw new ArgumentOutOfRangeException()
                };
                layoutCmds.SetTurnoutState(turnout.CurrentState);
                connection.QueueMsg(new MsgTurnoutState(connection, turnout));
            }
        }
    }

    /// <summary>
    /// Active a Route. You cannot inactivate a route, so the only
    /// option is command 2 - activate it.
    /// </summary>
    private async void SetRoute(string commandStr) {
        var routeID = commandStr[1..];
        if (connection.RailwayConfig?.Routes is { } routes) {
            var route = await routes.GetByIDAsync(routeID);
            if (route != null) {
                DeactiveRoutes();
                route.State = RouteState.Active;
                if ((RouteCommand)commandStr[0] == RouteCommand.Active) {
                    foreach (var turnoutEntry in route.RouteTurnouts) {
                        var turnout = connection.RailwayConfig.Turnouts?[turnoutEntry.TurnoutID];
                        if (turnout is { } item) {
                            var layoutCmds = new WitThrottleLayoutCmd(connection.ActiveController, item.Address);
                            item.CurrentState = turnoutEntry.State;
                            layoutCmds.SetTurnoutState(item.CurrentState);
                        }
                    }
                }
                connection.QueueMsg(new MsgRouteState(connection, route));
            }
        }
    }

    // Force all routes to be deactivated. However, could be more cleaver and could
    // only deactivate routes where there is a clash in the Turnouts.
    // ----------------------------------------------------------------------------
    private void DeactiveRoutes() {
        if (connection.RailwayConfig.Routes.Values is { } routes) {
            foreach (var route in routes) {
                route.State = RouteState.Inactive;
                connection.QueueMsg(new MsgRouteState(connection, route));
            }
        }
    }


    /// <summary>
    /// Looks at the 4th charater of the message and uses it to
    /// turn on or off the Power to the Layout. It then sends a PowerMsg
    /// update back to the client.
    /// </summary>
    /// <param name="state"></param>
    private void SetPowerState(char state) {
        var layoutCmds = new WitThrottleLayoutCmd(connection.ActiveController,null);
        switch (state) {
            case '0': if (layoutCmds.IsPowerSupported()) layoutCmds.SetPowerState(DCCPowerState.Off); break;
            case '1': if (layoutCmds.IsPowerSupported()) layoutCmds.SetPowerState(DCCPowerState.On);  break;
        }
        connection.QueueMsg(new MsgPowerState(connection));
    }
    public override string ToString() => $"CMD:Panel [{connection?.ConnectionID ?? 0}]";

    private enum TurnoutCommand {
        ToggleTurnout = '2',
        CloseTurnout  = 'C',
        ThrowTurnout  = 'T'
    }

    private enum RouteCommand {
        Active = '2',
    }

}