using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Locomotives(string prefix) : LayoutRepository<Locomotive>(prefix) {
    public Locomotives() : this("L") { }
}