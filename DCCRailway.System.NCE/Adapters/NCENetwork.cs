using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;

namespace DCCRailway.System.NCE.Adapters;

[Adapter("NCE Network Adapter", AdapterType.Network)]
public class NCENetwork : NetworkAdapter, IAdapter { }