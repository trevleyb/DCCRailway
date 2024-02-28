using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCmd {
    public DCCAccessoryState State   { get; set; }
}