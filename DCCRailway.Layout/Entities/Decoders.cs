using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Entities;

[Serializable]
public class Decoders(string prefix, string? filename = null, string? pathname = null)
    : LayoutRepository<Decoder>(prefix, filename, pathname);