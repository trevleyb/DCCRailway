using DCCRailway.Station.Adapters;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;

namespace DCCRailway.Station.NCE.Adapters;

[Adapter("NCE Network Adapter", AdapterType.Network)]
public class NCENetwork : NetworkAdapter, IAdapter { }