using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Blocks(string prefix) : LayoutRepository<Block>(prefix) {
    public Blocks() : this("B") { }
}