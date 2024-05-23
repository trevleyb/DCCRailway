using DCCRailway.Layout.Entities.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Decoders : LayoutRepository<Decoder> {
    public Decoders() : this(null) { }

    public Decoders(string? prefix = null) {
        Prefix = prefix ?? "D";
    }
}