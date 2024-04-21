using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdLocoSetFunctions : ICommand, ILocoCmd {
    public DCCFunctionBlocks Functions { get; }
}