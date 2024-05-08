using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Adapters.Base;

namespace DCCRailway.CmdStation.Exceptions;

public class ExpectedDataException : Exception {
    public ExpectedDataException(ICommand? command, IAdapter? adapter, string? message, Exception? ex = null) : base(adapter + ":" + message, ex) {
        Command = command;
        Adapter = adapter;
    }

    public IAdapter? Adapter { get; }

    public ICommand? Command { get; }
}