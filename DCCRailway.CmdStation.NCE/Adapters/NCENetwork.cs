using DCCRailway.CmdStation.Adapters;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.NCE.Adapters;

[Adapter("NCE Network Adapter", AdapterType.Network)]
public class NCENetwork : NetworkAdapter, IAdapter { }