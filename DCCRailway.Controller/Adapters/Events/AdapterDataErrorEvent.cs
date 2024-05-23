using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Adapters.Events;

public class DataErrorArgs : EventArgs, IAdapterEvent {
    public DataErrorArgs(string error, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Error   = error;
    }

    public string Error { get; init; }

    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }

    public override string ToString() {
        return
            $"ERROR: {Adapter?.AttributeInfo().Description ?? "Unknown Adapter"}:{Command?.ToString() ?? "Unknown Command"}<=={Error}";
    }
}