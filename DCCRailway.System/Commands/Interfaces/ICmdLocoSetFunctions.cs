using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdLocoSetFunctions : ICommand {
    public IDCCAddress       Address   { get; set; }
    public DCCFunctionBlocks Functions { get; }
}