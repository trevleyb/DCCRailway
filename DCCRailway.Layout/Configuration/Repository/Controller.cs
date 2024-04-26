using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Repository;

public class ControllerRepository(IEntityCollection<Controller> collection) : Repository<Guid,Controller>(collection) {

}