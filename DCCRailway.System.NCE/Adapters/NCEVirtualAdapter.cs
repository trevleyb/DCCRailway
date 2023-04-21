using System;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Attributes;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Adapters; 

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : VirtualAdapter, IAdapter {

    protected override byte[]? MapSimulatorResult(object? lastResult, ICommand command) {
        if (lastResult == null) return Array.Empty<byte>();
        return ((string) lastResult!).ToByteArray() ?? Array.Empty<byte>();
    }
}