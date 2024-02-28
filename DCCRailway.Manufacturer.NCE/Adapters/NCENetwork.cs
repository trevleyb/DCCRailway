using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;

namespace DCCRailway.Manufacturer.NCE.Adapters;

[Adapter("NCE Network Adapter", AdapterType.Network)]
public class NCENetwork : NetworkAdapter, IAdapter { }