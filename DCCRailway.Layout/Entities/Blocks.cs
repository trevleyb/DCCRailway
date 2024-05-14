using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Blocks(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Block>(prefix, filename, pathname);