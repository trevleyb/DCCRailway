using DCCRailway.Common.Utilities;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;

namespace DCCRailway.Station.Adapters.Events;

public class DataRecvArgs : EventArgs, IAdapterEvent {
    public DataRecvArgs(byte[]? data, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Data    = data;
    }

    public ICommand? Command { get; set; }
    public IAdapter? Adapter { get; set; }
    public byte[]?   Data    { get; }

    public override string ToString() => $"RECVDATA: {Adapter?.AttributeInfo().Description ?? "Unknown Adapter"}: {Command?.ToString() ?? "Unknown Command"}<=='{Data.ToDisplayValues()}'";
}