using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Locomotives(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Locomotive>(prefix, filename, pathname);