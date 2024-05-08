using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdTurnoutSet : ICommand, IAccyCmd {
    public DCCTurnoutState State { get; set; }
}