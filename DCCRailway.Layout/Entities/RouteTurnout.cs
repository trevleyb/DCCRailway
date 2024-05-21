using DCCRailway.Common.Types;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class RouteTurnout {
    public string          TurnoutID { get; set; }
    public DCCTurnoutState State     { get; set; }
}