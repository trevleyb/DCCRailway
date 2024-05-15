using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Blocks(string prefix, string name, string? pathname = null)
    : LayoutRepository<Block>(prefix, name, pathname);