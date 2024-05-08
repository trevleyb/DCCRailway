using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdAccySetState : ICommand, IAccyCmd {
    public DCCAccessoryState State { get; set; }
}