using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdTurnoutSet : ICommand, IAccyCmd {
    public DCCTurnoutState State { get; set; }
}