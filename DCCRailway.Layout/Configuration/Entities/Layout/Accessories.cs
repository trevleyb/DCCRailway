using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Accessories(string prefix = "ACCY") : EntityCollection<Accessory>(prefix);