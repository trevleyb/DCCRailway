using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Turnouts : LayoutRepository<Turnout> {
    public Turnouts() : this(null) { }
    public Turnouts(string? prefix= null) {
        Prefix = prefix ?? "T";
    }

}