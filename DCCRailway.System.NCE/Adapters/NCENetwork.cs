using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Attributes;

namespace DCCRailway.Systems.NCE.Adapters; 

[Adapter("NCE Network Adapter",AdapterType.Network)]
public class NCENetwork : NetworkAdapter, IAdapter {
}