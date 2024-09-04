using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("ROUTE={Id}, Name: {Name}")]
public class Route(string id = "") : LayoutEntity(id) {
    [JsonInclude] public List<RouteTurnout> RouteTurnouts = new();

    [JsonInclude] public RouteState State { get; set; } = RouteState.Unknown;

    // Add a route to the list of routes at the END of the list
    // ------------------------------------------------------------------------------------------------
    public void AddRoute(Turnout turnout, bool isThrown) {
        AddRoute(turnout.Id, isThrown);
    }

    public void AddRoute(string turnout, bool isThrown) {
        RouteTurnouts.Add(new RouteTurnout {
            TurnoutID = turnout, State = isThrown ? DCCTurnoutState.Thrown : DCCTurnoutState.Closed
        });
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

public enum RouteState {
    Active,
    Inactive,
    Unknown
}