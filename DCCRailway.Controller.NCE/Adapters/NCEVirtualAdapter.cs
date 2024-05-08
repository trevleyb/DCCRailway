using DCCRailway.Controller.Adapters;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : ConsoleAdapter, IAdapter { }