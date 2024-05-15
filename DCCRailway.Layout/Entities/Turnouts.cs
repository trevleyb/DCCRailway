using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Turnouts(string prefix, string name, string? pathname = null)
    : LayoutRepository<Turnout>(prefix, name, pathname);