﻿using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.Adapters.Events;

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