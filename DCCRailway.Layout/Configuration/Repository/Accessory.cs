using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository.Layout;

public class AccessoryRepository(IEntityCollection<Accessory> collection) :
    BaseRepository<Guid, Accessory>(collection) { }