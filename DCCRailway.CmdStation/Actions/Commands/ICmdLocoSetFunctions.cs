using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdLocoSetFunctions : ICommand, ILocoCmd {
    public DCCFunctionBlocks Functions { get; }
}