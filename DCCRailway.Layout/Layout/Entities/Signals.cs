using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Signals(string prefix) : LayoutRepository<Signal>(prefix) {
    public Signals() : this("SIG") { }
}