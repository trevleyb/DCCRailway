using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Turnouts(string prefix = "TURNOUT") : EntityCollection<Turnout>(prefix) { }