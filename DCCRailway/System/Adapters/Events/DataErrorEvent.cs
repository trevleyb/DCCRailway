﻿using DCCRailway.System.Commands;
using DCCRailway.Utilities;

namespace DCCRailway.System.Adapters.Events;

public class ErrorArgs : EventArgs, IAdapterEvent {
    public ErrorArgs(string error, IAdapter? adapter = null, ICommand? command = null) {
        Adapter = adapter;
        Command = command;
        Error   = error;
    }

    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
    public string    Error   { get; }

    public override string ToString() => $"ERROR: {Adapter?.AttributeInfo().Description ?? "Unknown Adapter"}:{Command?.ToString() ?? "Unknown Command"}<=={Error}";
}