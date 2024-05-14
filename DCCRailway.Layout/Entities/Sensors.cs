using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Sensors(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Sensor>(prefix, filename, pathname);