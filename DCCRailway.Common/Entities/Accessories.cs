using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Accessories : LayoutRepository<Accessory> {
    public Accessories() : this(null) { }

    public Accessories(string? prefix = null) {
        Prefix = prefix ?? "A";
    }
}