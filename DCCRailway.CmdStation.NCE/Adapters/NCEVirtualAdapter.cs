using DCCRailway.CmdStation.Adapters;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : ConsoleAdapter, IAdapter { }