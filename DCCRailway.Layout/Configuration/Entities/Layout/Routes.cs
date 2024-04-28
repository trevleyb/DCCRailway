using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Routes(string prefix = "ROUTE") : EntityCollection<Route>(prefix) { }