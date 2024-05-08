using DCCRailway.Controller.Adapters;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.NCE.Adapters;

[Adapter("NCE Network Adapter", AdapterType.Network)]
public class NCENetwork : NetworkAdapter, IAdapter { }