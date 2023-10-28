﻿using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Types;

namespace DCCRailway.System;

public interface ISystem {
    // Attach or detect an Adapter to a Command Station
    // ----------------------------------------------------------------------------
    public IAdapter?                          Adapter           { get; set; }
    public List<(Type command, string name)>? SupportedCommands { get; }
    public List<(Type adapter, string name)>? SupportedAdapters { get; }

    // Execute a Command. Must be executed via here
    // ----------------------------------------------------------------------------
    public IResult?  Execute(ICommand command);
    public IAdapter? CreateAdapter<T>() where T : IAdapter;
    public IAdapter? CreateAdapter(string name);

    // Create and Execute commands that are associated with this command station
    // --------------------------------------------------------------------------
    public TCommand? CreateCommand<TCommand>() where TCommand : ICommand;
    public TCommand? CreateCommand<TCommand>(int  value) where TCommand : ICommand;
    public TCommand? CreateCommand<TCommand>(byte value) where TCommand : ICommand;

    public IDCCAddress CreateAddress();
    public IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    public bool IsCommandSupported<T>() where T : ICommand;
}