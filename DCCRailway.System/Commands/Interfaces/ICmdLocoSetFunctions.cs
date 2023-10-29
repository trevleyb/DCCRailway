using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdLocoSetFunctions : ICommand,ILocoCommand {
    public IDCCAddress       Address   { get; set; }
    public DCCFunctionBlocks Functions { get; }
}