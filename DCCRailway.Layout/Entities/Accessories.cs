using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Accessories(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Accessory>(prefix, filename, pathname);