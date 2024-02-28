using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdLocoSetFunctions : ICommand, ILocoCmd {
    public DCCFunctionBlocks Functions { get; }
}