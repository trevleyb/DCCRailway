using System.Diagnostics;
using System.Net.Sockets;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("ROUTE={Id}, Name: {Name}")]
public class Route(string id = "") : BaseEntity(id) {
    public RouteState State { get; set; } = RouteState.Unknown;
    public List<RouteTurnout> RouteTurnouts = new();

    // Add a route to the list of routes at the END of the list
    // ------------------------------------------------------------------------------------------------
    public void AddRoute(Turnout turnout, bool isThrown) => AddRoute(turnout.Id,isThrown);
    public void AddRoute(string turnout, bool isThrown) {
        RouteTurnouts.Add(new RouteTurnout { TurnoutID = turnout, State = isThrown ? DCCTurnoutState.Thrown : DCCTurnoutState.Closed });
    }

    // Swap two turnouts in the list
    // -------------------------------------------------------------------------------------------------
    public void SwapTurnout(int firstPosition, int secondPosition) {
        var routesArray = RouteTurnouts.ToArray();
        if (firstPosition < routesArray.Length && secondPosition < routesArray.Length && firstPosition != secondPosition) {
            (routesArray[firstPosition], routesArray[secondPosition]) = (routesArray[secondPosition], routesArray[firstPosition]);
        }
        RouteTurnouts = routesArray.ToList();
    }
}

[Serializable]
public class RouteTurnout {
    public string TurnoutID { get; set; }
    public DCCTurnoutState State { get; set; }
}

public enum RouteState {
    Active,
    Inactive,
    Unknown
}