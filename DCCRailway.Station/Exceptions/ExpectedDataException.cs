using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Commands;

namespace DCCRailway.Station.Exceptions;

public class ExpectedDataException : Exception {
    public ExpectedDataException(ICommand? command, IAdapter? adapter, string? message, Exception? ex = null) : base(adapter + ":" + message, ex) {
        Command = command;
        Adapter = adapter;
    }

    public IAdapter? Adapter { get; }

    public ICommand? Command { get; }
}