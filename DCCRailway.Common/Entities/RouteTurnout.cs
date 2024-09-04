using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
public class RouteTurnout {
    public string          TurnoutID { get; set; }
    public DCCTurnoutState State     { get; set; }
}