using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Sensors(string prefix) : LayoutRepository<Sensor>(prefix){
    public Sensors() : this("S") { }
}