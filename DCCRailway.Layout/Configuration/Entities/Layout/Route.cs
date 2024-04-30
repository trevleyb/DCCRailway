using System.Diagnostics;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
[DebuggerDisplay("ROUTE={Id}, Name: {Name}")]
public class Route(string id = "") : BaseEntity(id) {
    public RouteState State { get; set; } = RouteState.Unknown;
}

public enum RouteState {
    Active,
    Inactive,
    Unknown
}