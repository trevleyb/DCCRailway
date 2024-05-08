using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdTurnoutSet : ICommand, IAccyCmd {
    public DCCTurnoutState State { get; set; }
}