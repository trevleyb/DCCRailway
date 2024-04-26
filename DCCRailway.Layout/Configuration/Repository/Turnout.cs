using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public class TurnoutRepository(IEntityCollection<Turnout> collection) : Repository<Guid, Turnout>(collection) { }