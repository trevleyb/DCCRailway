using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Blocks : LayoutRepository<Block> {
    public Blocks() : this(null) { }

    public Blocks(string? prefix = null) {
        Prefix = prefix ?? "B";
    }
}