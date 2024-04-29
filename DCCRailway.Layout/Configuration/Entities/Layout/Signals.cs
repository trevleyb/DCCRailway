using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Signals(string prefix = "SIG") : Repository<Signal>(prefix) {

}