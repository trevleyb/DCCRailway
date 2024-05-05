using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Sensors(string prefix) : LayoutRepository<Sensor>(prefix){
    public Sensors() : this("S") { }
}