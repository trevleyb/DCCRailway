using System.Diagnostics;
using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Accessories(string prefix) : Repository<Accessory>(prefix) {
    public Accessories() : this("ACCY") { }
}