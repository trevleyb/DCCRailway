using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;

namespace DCCRailway.DCCController.Controllers.Events;

public class ControllerEventCommandExec(ICommand command, ICommandResult result, IAdapter adapter, string message = "") : ControllerEventArgs(message), IControllerEventArgs{
    public ICommand Command { get; set; } = command;
    public IAdapter Adapter { get; set; } = adapter;
    public ICommandResult Result { get; set; } = result;
}