using System;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;

namespace DCCRailway.System.Manufacturer.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : ConsoleAdapter, IAdapter { }