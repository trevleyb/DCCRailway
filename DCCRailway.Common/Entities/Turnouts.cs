using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Turnouts : LayoutRepository<Turnout> {
    public Turnouts() : this(null) { }

    public Turnouts(string? prefix = null) {
        Prefix = prefix ?? "T";
    }

    public string TurnoutsLabel  { get; set; } = "Turnouts";
    public string TurnoutLabel   { get; set; } = "Turnout";
    public string StraightLabel  { get; set; } = "Closed";
    public string DivergingLabel { get; set; } = "Thrown";
}