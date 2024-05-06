using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Accessories(string prefix) : LayoutRepository<Accessory>(prefix) {
    public Accessories() : this("A") { }
}