using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Locomotives(string prefix) : LayoutRepository<Locomotive>(prefix) {
    public Locomotives() : this("L") { }
}