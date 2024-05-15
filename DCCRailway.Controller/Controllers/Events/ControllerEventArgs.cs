using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;

namespace DCCRailway.Controller.Controllers.Events;

public class ControllerEventArgs : EventArgs {
    public ControllerEventArgs(ICommand? command, ICmdResult? result, IAdapter? adapter, EventArgs? e, string? message = "") {
        Command = command;
        Result  = result;
        Adapter = adapter;
        Args    = e;
        Message = message;
    }

    public ControllerEventArgs(string message) => Message = message;

    public ICommand?   Command { get; set; }
    public ICmdResult? Result  { get; set; }
    public IAdapter?   Adapter { get; set; }
    public EventArgs?  Args    { get; set; }
    public string?     Message { get; set; }
}