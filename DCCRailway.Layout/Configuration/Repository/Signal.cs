using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public class SignalRepository(IEntityCollection<Signal> collection) : BaseRepository<Guid,Signal>(collection) {
}