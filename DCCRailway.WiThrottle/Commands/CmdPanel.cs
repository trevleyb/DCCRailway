using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities;
using DCCRailway.WiThrottle.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Commands;

public class CmdPanel(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd
{
    public void Execute(string commandStr)
    {
        logger.Information("WiThrottle Recieved Cmd from {0}: Panel {1}:{3}=>'{2}'", connection.ConnectionHandle,
                           ToString(), commandStr, connection.ToString());
        try
        {
            switch (commandStr[..3].ToUpper())
            {
            case "PPA":
                SetPowerState(commandStr[3]);
                break;
            case "PTA":
                ThrowTurnout(commandStr[3..]);
                break;
            case "PRA":
                SetRoute(commandStr[3..]);
                break;
            case "PFT":
                logger.Information("WiThrottle Recieved Cmd: Panel - {0}: Fast Clock not currently supported=>'{1}'",
                                   ToString(), commandStr);
                break;
            default:
                logger.Information("WiThrottle Recieved Cmd: Panel - {0}: Unknown Panel TurnoutCommand recieved=>'{1}'",
                                   ToString(), commandStr);
                break;
            }

            ;
        }
        catch
        {
            logger.Error("WiThrottle Recieved Cmd: Panel - {0}: Unable to Process the command =>'{1}'", ToString(),
                         commandStr);
        }
    }

    /// <summary>
    ///     See if we can find the turnout ID provided and if we can, then
    ///     throw to switch it and finally send a message to tell the throttle
    ///     of its current state.
    /// </summary>
    private void ThrowTurnout(string commandStr)
    {
        var turnoutID = commandStr[1..];
        if (connection.RailwaySettings.Turnouts is { } turnouts)
        {
            var turnout    = turnouts.GetByID(turnoutID);
            var layoutCmds = new LayoutCmdHelper(connection.CommandStation, turnout?.Address);

            if (turnout != null)
            {
                turnout.CurrentState = (TurnoutCommand)commandStr[0] switch
                {
                    TurnoutCommand.ToggleTurnout => turnout.CurrentState == DCCTurnoutState.Closed
                        ? DCCTurnoutState.Thrown
                        : DCCTurnoutState.Closed,
                    TurnoutCommand.CloseTurnout => DCCTurnoutState.Closed,
                    TurnoutCommand.ThrowTurnout => DCCTurnoutState.Thrown,
                    _                           => throw new ArgumentOutOfRangeException()
                };
                layoutCmds.SetTurnoutState(turnout.CurrentState);
                connection.QueueMsgToAll(new MsgTurnoutState(connection, turnout));
            }
        }
    }

    /// <summary>
    ///     Active a Route. You cannot inactivate a route, so the only
    ///     option is command 2 - activate it.
    /// </summary>
    private void SetRoute(string commandStr)
    {
        var routeID = commandStr[1..];
        if (connection.RailwaySettings.Routes is { } routes)
        {
            var route = routes.GetByID(routeID);
            if (route != null)
            {
                DeactiveRoutes(route);
                route.State = RouteState.Active;
                if ((RouteCommand)commandStr[0] == RouteCommand.Active)
                {
                    foreach (var turnoutEntry in route.RouteTurnouts)
                    {
                        var turnout = connection.RailwaySettings.Turnouts?[turnoutEntry.TurnoutID];
                        if (turnout is { } item)
                        {
                            var layoutCmds = new LayoutCmdHelper(connection.CommandStation, item.Address);
                            item.CurrentState = turnoutEntry.State;
                            layoutCmds.SetTurnoutState(item.CurrentState);
                        }
                    }
                }

                connection.QueueMsgToAll(new MsgRouteState(connection, route));
            }
        }
    }

    // Force all routes to be deactivated. However, could be more cleaver and could
    // only deactivate routes where there is a clash in the Turnouts.
    // ----------------------------------------------------------------------------
    private void DeactiveRoutes(Route refRoute)
    {
        // look to see if any of the Turnouts in the reference Route (refRoute) are contained
        // on any other ACTIVE route. If they are, and if the turnout direct is different,
        // then we need to DEACTIVATE the existing active routes first. We don't need to
        // actually throw the turnouts, just mark the turnout as de-activated
        // ----------------------------------------------------------------------------------
        var deactivateList = new List<Route>();
        if (connection.RailwaySettings.Routes.Values is { } routes)
        {
            foreach (var route in routes)
            {
                if (AreTurnoutsMatched(refRoute, route))
                {
                    deactivateList.Add(route);
                    route.State = RouteState.Inactive;
                }
            }

            foreach (var route in deactivateList)
            {
                connection.QueueMsgToAll(new MsgRouteState(connection, route));
            }
        }
    }

    private bool AreTurnoutsMatched(Route refRoute, Route route)
    {
        foreach (var turnout in refRoute.RouteTurnouts)
        {
            foreach (var checkTurnout in route.RouteTurnouts)
            {
                if (turnout.TurnoutID == checkTurnout.TurnoutID && turnout.State != checkTurnout.State)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    ///     Looks at the 4th charater of the message and uses it to
    ///     turn on or off the Power to the Entities. It then sends a PowerMsg
    ///     update back to the client.
    /// </summary>
    /// <param name="state"></param>
    private void SetPowerState(char state)
    {
        var layoutCmds = new LayoutCmdHelper(connection.CommandStation);
        switch (state)
        {
        case '0':
            if (layoutCmds.IsPowerSupported()) layoutCmds.SetPowerState(DCCPowerState.Off);
            break;
        case '1':
            if (layoutCmds.IsPowerSupported()) layoutCmds.SetPowerState(DCCPowerState.On);
            break;
        }

        connection.QueueMsgToAll(new MsgPowerState(connection));
    }

    public override string ToString() => "CMD:Panel";

    private enum TurnoutCommand
    {
        ToggleTurnout = '2',
        CloseTurnout  = 'C',
        ThrowTurnout  = 'T'
    }

    private enum RouteCommand
    {
        Active = '2'
    }
}