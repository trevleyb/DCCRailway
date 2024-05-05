using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Blocks(string prefix) : LayoutRepository<Block>(prefix) {
    public Blocks() : this("B") { }
}