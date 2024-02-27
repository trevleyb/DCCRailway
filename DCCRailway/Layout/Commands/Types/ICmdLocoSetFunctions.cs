using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetFunctions : ICommand, ILocoCmd {
    public DCCFunctionBlocks Functions { get; }
}