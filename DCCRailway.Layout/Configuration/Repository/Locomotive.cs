using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository.Layout;

public class LocomotiveRepository(IEntityCollection<Locomotive> collection) : BaseRepository<Guid, Locomotive>(collection) { }