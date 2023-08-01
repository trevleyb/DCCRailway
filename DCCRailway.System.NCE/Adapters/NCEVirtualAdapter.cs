using System;
using DCCRailway.Core.Utilities;

namespace DCCRailway.System.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : VirtualAdapter, IAdapter {
    protected override byte[]? MapSimulatorResult(object? lastResult, ICommand command) {
        if (lastResult == null) return Array.Empty<byte>();

        return ((string)lastResult!).ToByteArray() ?? Array.Empty<byte>();
    }
}