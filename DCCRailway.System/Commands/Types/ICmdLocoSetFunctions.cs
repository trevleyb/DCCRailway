using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdLocoSetFunctions : ICommand,ILocoCommand {
    public DCCFunctionBlocks Functions { get; }
}