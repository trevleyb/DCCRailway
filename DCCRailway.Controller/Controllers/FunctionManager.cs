using DCCRailway.Common.Types;
using Serilog;

namespace DCCRailway.Controller.Controllers;

public class FunctionManager(ILogger logger, ICommandStation commandStation) {
    private readonly Dictionary<int, DCCFunctionBlocks> _functionBlocks = new();

    public DCCFunctionBlocks FunctionBlocks(DCCAddress address) {
        return FunctionBlocks(address.Address);
    }

    public DCCFunctionBlocks FunctionBlocks(int address) {
        if (!_functionBlocks.ContainsKey(address)) {
            var blocks = new DCCFunctionBlocks();
            _functionBlocks.Add(address, blocks);
        }

        return _functionBlocks[address];
    }
}