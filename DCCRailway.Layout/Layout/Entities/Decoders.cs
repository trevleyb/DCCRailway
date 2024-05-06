using DCCRailway.Layout.Layout.Collection;

namespace DCCRailway.Layout.Layout.Entities;

[Serializable]
public class Decoders(string prefix = "D") : LayoutRepository<Decoder>(prefix) {
    public Decoders() : this("D") { }
}