using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public class SensorRepository(IEntityCollection<Sensor> collection) : BaseRepository<Guid,Sensor>(collection) {
}