using DCCRailway.System.Commands;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Adapters.Events;

public class DataRecvArgs : EventArgs {
    public DataRecvArgs(byte[]? data, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Data = data;
    }

    public ICommand? Command { get; set; }
    public IAdapter? Adapter { get; set; }
    public byte[]? Data { get; }

    public override string ToString() {
        return $"RECVDATA: {Adapter?.Info().Description ?? "Unknown Adapter"}: {Command?.ToString() ?? "Unknown Command"}<=='{Data.ToDisplayValues()}'";
    }
}