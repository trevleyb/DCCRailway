using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetFunctions : ICommand,ILocoCommand {
    public DCCFunctionBlocks Functions { get; }
}