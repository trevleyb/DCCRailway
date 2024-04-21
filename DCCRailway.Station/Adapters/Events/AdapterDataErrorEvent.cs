using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;

namespace DCCRailway.Station.Adapters.Events;

public class DataErrorArgs : EventArgs, IAdapterEvent {
    public DataErrorArgs(string error, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Error   = error;
    }

    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
    public string    Error   { get; init; }

    public override string ToString() => $"ERROR: {Adapter?.AttributeInfo().Description ?? "Unknown Adapter"}:{Command?.ToString() ?? "Unknown Command"}<=={Error}";
}