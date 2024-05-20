using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Blocks : LayoutRepository<Block> {
    public Blocks() : this(null) { }
    public Blocks(string? prefix = null) {
        Prefix = prefix ?? "B";
    }
}