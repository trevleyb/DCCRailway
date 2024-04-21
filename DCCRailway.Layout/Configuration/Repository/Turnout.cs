using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository.Layout;

public class TurnoutRepository(IEntityCollection<Turnout> collection) : BaseRepository<Guid, Turnout>(collection) { }