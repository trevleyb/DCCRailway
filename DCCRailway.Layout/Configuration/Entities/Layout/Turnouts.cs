using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Turnouts(string prefix) : Repository<Turnout>(prefix) {
    public Turnouts() : this("TRN") { }
}