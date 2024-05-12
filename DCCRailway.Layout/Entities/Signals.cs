using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Signals(string prefix) : LayoutRepository<Signal>(prefix) {
    public Signals() : this("SIG") { }
}