using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Sensors(string prefix) : LayoutRepository<Sensor>(prefix){
    public Sensors() : this("S") { }
}