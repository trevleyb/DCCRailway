using System;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : VirtualAdapter, IAdapter {
    protected byte[]? MapSimulatorResult(object? lastResult, ICommand command) {
        if (lastResult == null) return Array.Empty<byte>();
        return ((string)lastResult!).ToByteArray() ?? Array.Empty<byte>();
    }
}