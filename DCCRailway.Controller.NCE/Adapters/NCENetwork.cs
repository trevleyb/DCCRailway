using DCCRailway.Common.Types;
using DCCRailway.Controller.Adapters;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using Serilog;

namespace DCCRailway.Controller.NCE.Adapters;

[Adapter("NCE Network Adapter", AdapterType.Network)]
public class NCENetwork(ILogger logger) : NetworkAdapter(logger), IAdapter {
    public DCCFunctionBlocks LastFunctionBlocks { get; set; }
}