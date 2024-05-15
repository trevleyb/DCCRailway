using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Decoders(string prefix, string name, string? pathname = null)
    : LayoutRepository<Decoder>(prefix, name, pathname);