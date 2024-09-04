using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Decoders : LayoutRepository<Decoder> {
    public Decoders() : this(null) { }

    public Decoders(string? prefix = null) {
        Prefix = prefix ?? "D";
    }
}