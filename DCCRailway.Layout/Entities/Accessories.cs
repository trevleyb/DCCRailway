using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Accessories : LayoutRepository<Accessory> {
    public Accessories() : this(null) { }

    public Accessories(string? prefix = null) {
        Prefix = prefix ?? "A";
    }
}