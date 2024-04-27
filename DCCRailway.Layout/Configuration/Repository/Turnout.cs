using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Collection;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Repository.Base;

namespace DCCRailway.Layout.Configuration.Repository;

public class TurnoutRepository(IEntityCollection<Turnout> collection) : Repository<Turnout>(collection) { }