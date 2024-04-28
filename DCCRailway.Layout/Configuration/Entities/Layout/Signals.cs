using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Signals(string prefix = "SIGNAL") : EntityCollection<Signal>(prefix) {

}