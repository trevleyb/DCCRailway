using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdLocoSetFunctions : ICommand,ILocoCommand {
    public IDCCAddress       Address   { get; set; }
    public DCCFunctionBlocks Functions { get; }
}