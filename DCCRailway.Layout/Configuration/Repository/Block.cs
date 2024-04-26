using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public class BlockRepository(IEntityCollection<Block> collection) : Repository<Guid,Block>(collection) {

}