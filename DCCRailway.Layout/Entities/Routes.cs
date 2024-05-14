using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Routes(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Route>(prefix, filename, pathname);