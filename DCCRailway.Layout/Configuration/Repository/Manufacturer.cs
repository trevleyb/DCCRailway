using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Configuration.Repository;

public class ManufacturerRepository(IEntityCollection<Manufacturer> collection) : Repository<byte,Manufacturer>(collection) {
}