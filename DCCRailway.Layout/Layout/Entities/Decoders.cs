using DCCRailway.LayoutService.Layout.Collection;

namespace DCCRailway.LayoutService.Layout.Entities;

[Serializable]
public class Decoders(string prefix = "D") : LayoutRepository<Decoder>(prefix) {
    public Decoders() : this("D") { }
}