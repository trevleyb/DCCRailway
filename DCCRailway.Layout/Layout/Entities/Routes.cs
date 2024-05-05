using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Routes(string prefix) : LayoutRepository<Route>(prefix) {
    public Routes() : this("R") { }
}