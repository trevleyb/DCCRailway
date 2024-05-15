using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Signals(string prefix, string name, string? pathname = null)
    : LayoutRepository<Signal>(prefix, name, pathname) {

    public string Version { get; set; } = "v1.0";

}