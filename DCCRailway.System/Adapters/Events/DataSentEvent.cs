﻿using DCCRailway.System.Commands;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Adapters.Events;

public class DataSentArgs : EventArgs {
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

        return $"SENTDATA: {Adapter?.Info().Description ?? "Unknown Adapter"}: {Command?.ToString() ?? "Unknown Command"}==>'{Data.ToDisplayValues()}'";
    }
}