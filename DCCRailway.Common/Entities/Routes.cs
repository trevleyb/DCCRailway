using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Routes : LayoutRepository<Route> {
    public Routes() : this(null) { }

    public Routes(string? prefix = null) {
        Prefix = prefix ?? "R";
    }

    public string RoutesLabel         { get; set; } = "Routes";
    public string RouteLabel          { get; set; } = "Route";
    public string ActiveLabel         { get; set; } = "Active";
    public string InActiveLabel       { get; set; } = "Inactive";
    public bool   DeActivateOnOverlap { get; set; } = true;
}