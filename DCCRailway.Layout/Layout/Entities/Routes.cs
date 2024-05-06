using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Routes(string prefix) : LayoutRepository<Route>(prefix) {
    public Routes() : this("R") { }
}