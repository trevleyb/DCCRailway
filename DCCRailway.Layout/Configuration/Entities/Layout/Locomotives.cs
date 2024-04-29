using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Locomotives(string prefix = "LOCO") : Repository<Locomotive>(prefix) {

}