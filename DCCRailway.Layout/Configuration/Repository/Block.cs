using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository.Layout;

public class BlockRepository(IEntityCollection<Block> collection) : BaseRepository<Guid,Block>(collection) {

}