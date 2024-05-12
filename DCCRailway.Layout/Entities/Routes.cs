using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Routes(string prefix) : LayoutRepository<Route>(prefix) {
    public Routes() : this("R") { }
}