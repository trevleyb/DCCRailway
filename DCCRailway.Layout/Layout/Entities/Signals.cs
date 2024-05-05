using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Signals(string prefix) : LayoutRepository<Signal>(prefix) {
    public Signals() : this("SIG") { }
}