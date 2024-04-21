using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;

namespace DCCRailway.Station.Controllers.Events;

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