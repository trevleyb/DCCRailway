using System;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;

namespace DCCRailway.System.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : ConsoleAdapter, IAdapter { }