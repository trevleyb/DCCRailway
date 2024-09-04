using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Locomotives : LayoutRepository<Locomotive> {
    public Locomotives() : this(null) { }

    public Locomotives(string? prefix = null) {
        Prefix = prefix ?? "L";
    }
}