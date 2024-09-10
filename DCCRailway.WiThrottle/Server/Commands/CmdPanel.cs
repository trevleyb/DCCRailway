using DCCRailway.Common.Entities;
using DCCRailway.Common.Types;
using DCCRailway.WiThrottle.Server.Messages;
using Serilog;

namespace DCCRailway.WiThrottle.Server.Commands;

public class CmdPanel(ILogger logger, Connection connection) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        logger.Information("WiThrottle Recieved Cmd from [{0}]: Panel {1}:{3}=>'{2}'", connection.ConnectionHandle, ToString(), commandStr, connection.ToString());

        try {
            var command = commandStr[..3].ToUpper();
            switch (command) {
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
                logger.Information("WiThrottle Recieved Cmd: Panel - {0}: Fast Clock. Should not get this from a client.=>'{1}'", ToString(), commandStr);
                break;
            default:
                logger.Information("WiThrottle Recieved Cmd: Panel - {0}: Unknown Panel TurnoutCommand recieved=>'{1}'", ToString(), commandStr);
                break;
            }
        } catch (Exception ex) {
            logger.Error("WiThrottle Recieved Cmd: Panel - {0}: Unable to Process the command =>'{1}' due to: {2}", ToString(), commandStr, ex.Message);
        }
    }

    /// <summary>
    ///     See if we can find the turnout ID provided and if we can, then
    ///     throw to switch it and finally send a message to tell the throttle
    ///     of its current state.
    /// </summary>
    private void ThrowTurnout(string commandStr) {
        var turnoutID = commandStr[1..];

        // See if there is an entry for this turnout in our collection of Turnouts
        // If there is, use that so we can toggle the state if we can, otherwise
        // use the ID that has been passed as the Address of the turnout. 
        // -------------------------------------------------------------------------
        if (connection.RailwaySettings.Turnouts is { } turnouts) {
            var turnout = turnouts.GetByID(turnoutID) ?? null;
            if (turnout is null) {
                turnout = new Turnout(turnoutID) {
                    Name         = "Turnout " + turnoutID,
                    Address      = new DCCAddress(turnoutID),
                    CurrentState = DCCTurnoutState.Closed,
                    IsManual     = true
                };

                turnouts.Add(turnout);
            }

            if (turnout.Address.Address > 0) {
                var layoutCmds = new LayoutCmdHelper(connection.CommandStation, turnout.Address);
                var state      = commandStr[0];

                switch (state) {
                case '2':
                    turnout.CurrentState = (TurnoutCommand)commandStr[0] switch {
                        TurnoutCommand.ToggleTurnout => turnout.CurrentState == DCCTurnoutState.Closed ? DCCTurnoutState.Thrown : DCCTurnoutState.Closed,
                        TurnoutCommand.CloseTurnout  => DCCTurnoutState.Closed,
                        TurnoutCommand.ThrowTurnout  => DCCTurnoutState.Thrown,
                        _                            => throw new ArgumentOutOfRangeException()
                    };

                    layoutCmds.SetTurnoutState(turnout.CurrentState);
                    connection.QueueMsgToAll(new MsgTurnoutState(connection, turnout));
                    break;
                case 'C':
                    turnout.CurrentState = DCCTurnoutState.Closed;
                    layoutCmds.SetTurnoutState(DCCTurnoutState.Closed);
                    connection.QueueMsgToAll(new MsgTurnoutState(connection, turnoutID, DCCTurnoutState.Closed));
                    break;
                case 'T':
                    turnout.CurrentState = DCCTurnoutState.Thrown;
                    layoutCmds.SetTurnoutState(DCCTurnoutState.Thrown);
                    connection.QueueMsgToAll(new MsgTurnoutState(connection, turnoutID, DCCTurnoutState.Thrown));
                    break;
                default:
                    break;
                }
            }
        }
    }

    /// <summary>
    ///     Active a Route. You cannot inactivate a route, so the only
    ///     option is command 2 - activate it.
    /// </summary>
    private void SetRoute(string commandStr) {
        var routeID = commandStr[1..];

        if (connection.RailwaySettings.TrackRoutes is { } routes) {
            var route = routes.GetByID(routeID);

            if (route != null) {
                DeactiveRoutes(route);
                route.State = RouteState.Active;

                if ((RouteCommand)commandStr[0] == RouteCommand.Active) {
                    foreach (var turnoutEntry in route.RouteTurnouts) {
                        var turnout = connection.RailwaySettings.Turnouts.GetByID(turnoutEntry.TurnoutID);
                        if (turnout is { } turnoutItem) {
                            var layoutCmds = new LayoutCmdHelper(connection.CommandStation, turnoutItem.Address);
                            turnoutItem.CurrentState = turnoutEntry.State;
                            connection.QueueMsgToAll(new MsgTurnoutState(connection, turnoutItem));
                            layoutCmds.SetTurnoutState(turnoutItem.CurrentState);
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
    private void DeactiveRoutes(TrackRoute refTrackRoute) {
        // look to see if any of the Turnouts in the reference Route (refRoute) are contained
        // on any other ACTIVE route. If they are, and if the turnout direct is different,
        // then we need to DEACTIVATE the existing active routes first. We don't need to
        // actually throw the turnouts, just mark the turnout as de-activated
        // ----------------------------------------------------------------------------------
        var deactivateList = new List<TrackRoute>();

        if (connection.RailwaySettings.TrackRoutes is { } routes) {
            foreach (var route in routes) {
                if (AreTurnoutsMatched(refTrackRoute, route)) {
                    deactivateList.Add(route);
                    route.State = RouteState.Inactive;
                }
            }

            foreach (var route in deactivateList) connection.QueueMsgToAll(new MsgRouteState(connection, route));
        }
    }

    private bool AreTurnoutsMatched(TrackRoute refTrackRoute, TrackRoute trackRoute) {
        foreach (var turnout in refTrackRoute.RouteTurnouts)
        foreach (var checkTurnout in trackRoute.RouteTurnouts) {
            if (turnout.TurnoutID == checkTurnout.TurnoutID && turnout.State != checkTurnout.State) {
                return true;
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
    private void SetPowerState(char state) {
        var layoutCmds = new LayoutCmdHelper(connection.CommandStation);

        switch (state) {
        case '0':
            if (layoutCmds.IsPowerSupported()) layoutCmds.SetPowerState(DCCPowerState.Off);
            break;
        case '1':
            if (layoutCmds.IsPowerSupported()) layoutCmds.SetPowerState(DCCPowerState.On);
            break;
        }

        connection.QueueMsgToAll(new MsgPowerState(connection));
    }

    public override string ToString() {
        return "CMD:Panel";
    }

    private enum TurnoutCommand {
        ToggleTurnout = '2',
        CloseTurnout  = 'C',
        ThrowTurnout  = 'T'
    }

    private enum RouteCommand {
        Active = '2'
    }
}