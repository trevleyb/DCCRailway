using DCCRailway.Station.Adapters;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;

namespace DCCRailway.Station.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : ConsoleAdapter, IAdapter { }