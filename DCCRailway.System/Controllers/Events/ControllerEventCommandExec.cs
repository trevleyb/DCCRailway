using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Controllers.Events;

public class ControllerEventCommandExec(ICommand command, ICommandResult result, IAdapter adapter, string message = "") : ControllerEventArgs(message), IControllerEventArgs{
    public ICommand Command { get; set; } = command;
    public IAdapter Adapter { get; set; } = adapter;
    public ICommandResult Result { get; set; } = result;
}