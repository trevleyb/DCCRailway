﻿using DCCRailway.Layout.Adapters;

namespace DCCRailway.Manufacturer.NCE.Adapters;

[Adapter("NCE Network Adapter", AdapterType.Network)]
public class NCENetwork : NetworkAdapter, IAdapter { }