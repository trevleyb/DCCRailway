using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Controllers.Events;

public class ControllerEventArgs : EventArgs {

    public ControllerEventArgs (ICommand? command, IAdapter? adapter, ICommandResult? result, EventArgs? e, string message ="") {
        Command = command;
        Adapter = adapter;
        Result  = result;
        Args    = e;
        Message = message;
    }

    public ControllerEventArgs (string message) {
        Message = message;
    }

    public ICommand?       Command { get; set; }
    public IAdapter?       Adapter { get; set; }
    public ICommandResult? Result  { get; set; }
    public EventArgs?      Args    { get; set; }
    public string?         Message { get; set; }

}