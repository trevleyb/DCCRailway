using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Adapters.Events;

public class DataSentArgs : EventArgs, IAdapterEvent {
    public DataSentArgs(byte[]? data, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Data    = data;
    }

    public byte[]? Data { get; }

    public ICommand? Command { get; set; }
    public IAdapter? Adapter { get; set; }

    public override string ToString() {
        var tmp = Data.ToDisplayValueChars();

        return
            $"SENTDATA: {Adapter?.AttributeInfo().Description ?? "Unknown Adapter"}: {Command?.ToString() ?? "Unknown Command"}==>'{Data.ToDisplayValues()}'";
    }
}