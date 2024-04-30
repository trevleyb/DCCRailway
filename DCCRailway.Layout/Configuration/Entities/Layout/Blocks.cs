using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Blocks(string prefix) : Repository<Block>(prefix) {
    public Blocks() : this("BLK") { }
}