using System;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.NCE.Adapters;

[Adapter("NCE Virtual Adapter", AdapterType.Virtual)]
public class NCEVirtualAdapter : VirtualAdapter, IAdapter {
    protected byte[]? MapSimulatorResult(object? lastResult, ICommand command) {
        if (lastResult == null) return Array.Empty<byte>();
        return ((string)lastResult!).ToByteArray() ?? Array.Empty<byte>();
    }
}