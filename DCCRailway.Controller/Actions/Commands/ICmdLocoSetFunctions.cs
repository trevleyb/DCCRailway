using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdLocoSetFunctions : ICommand, ILocoCmd {
    public DCCFunctionBlocks Functions { get; }
}