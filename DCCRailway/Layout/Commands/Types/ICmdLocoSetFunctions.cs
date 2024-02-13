using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetFunctions : ICommand,ILocoCommand {
    public DCCFunctionBlocks Functions { get; }
}