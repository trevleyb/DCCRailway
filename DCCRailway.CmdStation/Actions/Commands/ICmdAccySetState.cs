using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdAccySetState : ICommand, IAccyCmd {
    public DCCAccessoryState State { get; set; }
}