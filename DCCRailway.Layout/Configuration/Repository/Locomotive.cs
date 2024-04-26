using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public class LocomotiveRepository(IEntityCollection<Locomotive> collection) : Repository<Locomotive>(collection) { }