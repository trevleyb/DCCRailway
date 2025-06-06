using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Entities;

[Serializable]
public class Signals : LayoutRepository<Signal> {
    public Signals() : this(null) { }

    public Signals(string? prefix = null) {
        Prefix = prefix ?? "X";
    }

    public string Version { get; set; } = "v1.0";
}