using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Turnouts(string prefix) : LayoutRepository<Turnout>(prefix) {
    public Turnouts() : this("T") { }
}