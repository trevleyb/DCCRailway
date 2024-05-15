using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Routes(string prefix, string name, string? pathname = null)
    : LayoutRepository<Route>(prefix, name, pathname);