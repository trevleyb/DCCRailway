using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Signals(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Signal>(prefix, filename, pathname);