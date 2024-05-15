using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Sensors(string prefix, string name, string? pathname = null)
    : LayoutRepository<Sensor>(prefix, name, pathname);