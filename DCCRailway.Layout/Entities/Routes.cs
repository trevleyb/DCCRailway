using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Routes : LayoutRepository<Route> {
    public Routes() : this(null) { }
    public Routes(string? prefix= null) {
        Prefix = prefix ?? "R";
    }
}