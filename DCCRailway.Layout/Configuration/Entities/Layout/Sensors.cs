using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Sensors(string prefix) : Repository<Sensor>(prefix){
    public Sensors() : this("S") { }
}