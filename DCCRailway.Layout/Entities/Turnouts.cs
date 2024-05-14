using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Turnouts(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Turnout>(prefix, filename, pathname);