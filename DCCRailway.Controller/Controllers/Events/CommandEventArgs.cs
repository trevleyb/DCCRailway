using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;

namespace DCCRailway.Controller.Controllers.Events;

public class CommandEventArgs(ICommand? command, ICmdResult? result, string? message = "")
    : ControllerEventArgs(command, result, null, null, message) {
    public CommandEventArgs(ICmdResult result)
        : this(result.Command, result, null) { }
}