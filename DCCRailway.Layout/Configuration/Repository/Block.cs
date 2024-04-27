using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Collection;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Repository.Base;

namespace DCCRailway.Layout.Configuration.Repository;

public class BlockRepository(IEntityCollection<Block> collection) : Repository<Block>(collection) {

}