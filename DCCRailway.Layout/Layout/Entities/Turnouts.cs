using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Turnouts(string prefix) : LayoutRepository<Turnout>(prefix) {
    public Turnouts() : this("T") { }
}