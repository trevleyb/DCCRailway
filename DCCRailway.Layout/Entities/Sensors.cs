using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Sensors : LayoutRepository<Sensor> {
    public Sensors() : this(null) { }
    public Sensors(string? prefix= null) {
        Prefix = prefix ?? "S";
    }
}