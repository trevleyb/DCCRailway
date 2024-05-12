using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Accessories(string prefix) : LayoutRepository<Accessory>(prefix) {
    public Accessories() : this("A") { }
}