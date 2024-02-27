using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;

namespace DCCRailway.Layout.Controllers.Events;

public class ControllerEventCommandExec(ICommand command, ICommandResult result, IAdapter adapter, string message = "") : ControllerEventArgs(message), IControllerEventArgs{
    public ICommand Command { get; set; } = command;
    public IAdapter Adapter { get; set; } = adapter;
    public ICommandResult Result { get; set; } = result;
}