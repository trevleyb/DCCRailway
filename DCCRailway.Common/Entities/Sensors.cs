using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Sensors : LayoutRepository<Sensor> {
    public Sensors() : this(null) { }

    public Sensors(string? prefix = null) {
        Prefix = prefix ?? "S";
    }
}