using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Locomotives(string prefix, string name, string? pathname = null)
    : LayoutRepository<Locomotive>(prefix, name, pathname);