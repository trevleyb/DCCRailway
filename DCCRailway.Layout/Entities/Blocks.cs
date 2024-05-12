using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Blocks(string prefix) : LayoutRepository<Block>(prefix) {
    public Blocks() : this("B") { }
}