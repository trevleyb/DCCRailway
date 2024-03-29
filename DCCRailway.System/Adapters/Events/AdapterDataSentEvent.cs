﻿using DCCRailway.Common.Utilities;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;

namespace DCCRailway.System.Adapters.Events;

public class DataSentArgs : EventArgs, IAdapterEvent {
    public DataSentArgs(byte[]? data, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Data    = data;
    }

    public ICommand? Command { get; set; }
    public IAdapter? Adapter { get; set; }
    public byte[]?   Data    { get; }

    public override string ToString() {
        var tmp = Data.ToDisplayValueChars();

        return $"SENTDATA: {Adapter?.AttributeInfo().Description ?? "Unknown Adapter"}: {Command?.ToString() ?? "Unknown Command"}==>'{Data.ToDisplayValues()}'";
    }
}