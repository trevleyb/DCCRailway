using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdLocoSetFunctions : ICommand,ILocoCommand {
    public DCCFunctionBlocks Functions { get; }
}