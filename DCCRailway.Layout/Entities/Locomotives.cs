using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Locomotives : LayoutRepository<Locomotive> {
    public Locomotives() : this(null) { }

    public Locomotives(string? prefix = null) {
        Prefix = prefix ?? "L";
    }
}