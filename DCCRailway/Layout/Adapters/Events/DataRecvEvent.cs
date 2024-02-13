using DCCRailway.Layout.Commands;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.Adapters.Events;

public class DataRecvArgs : EventArgs, IAdapterEvent {
    public DataRecvArgs(byte[]? data, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Data    = data;
    }

    public ICommand? Command { get; set; }
    public IAdapter? Adapter { get; set; }
    public byte[]?   Data    { get; }

    public override string ToString() => $"RECVDATA: {Adapter?.Info().Description ?? "Unknown Adapter"}: {Command?.ToString() ?? "Unknown Command"}<=='{Data.ToDisplayValues()}'";
}