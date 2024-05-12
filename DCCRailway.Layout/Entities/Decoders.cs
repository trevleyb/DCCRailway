using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Decoders(string prefix = "D") : LayoutRepository<Decoder>(prefix) {
    public Decoders() : this("D") { }
}