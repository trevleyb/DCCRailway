using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Turnouts(string prefix) : LayoutRepository<Turnout>(prefix) {
    public Turnouts() : this("T") { }
}