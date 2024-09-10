using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class TrackRoutes : LayoutRepository<TrackRoute> {
    public TrackRoutes() : this(null) { }

    public TrackRoutes(string? prefix = null) {
        Prefix = prefix ?? "R";
    }

    public string RoutesLabel         { get; set; } = "Routes";
    public string RouteLabel          { get; set; } = "Route";
    public string ActiveLabel         { get; set; } = "Active";
    public string InActiveLabel       { get; set; } = "Inactive";
    public bool   DeActivateOnOverlap { get; set; } = true;
}