using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Routes(string prefix = "RTE") : Repository<Route>(prefix) { }