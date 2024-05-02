using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Decoders(string prefix = "D") : Repository<Decoder>(prefix) {
    public Decoders() : this("D") { }
}