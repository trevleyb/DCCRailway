using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Locomotives(string prefix) : LayoutRepository<Locomotive>(prefix) {
    public Locomotives() : this("L") { }
}