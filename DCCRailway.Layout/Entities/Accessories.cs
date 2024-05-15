using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Accessories(string prefix, string name, string? pathname = null)
    : LayoutRepository<Accessory>(prefix, name, pathname);