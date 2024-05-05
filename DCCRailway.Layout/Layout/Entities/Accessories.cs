using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Accessories(string prefix) : LayoutRepository<Accessory>(prefix) {
    public Accessories() : this("A") { }
}