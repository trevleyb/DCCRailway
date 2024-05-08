﻿using System.Reflection;
using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Controllers.Events;
using DCCRailway.CmdStation.Exceptions;
using DCCRailway.CmdStation.Helpers;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using Serilog;

namespace DCCRailway.CmdStation.Controllers;

public abstract class Controller : IController, IParameterMappable {

    public event EventHandler<ControllerEventArgs> ControllerEvent;

    private CommandManager  _commands { get; } = new(Assembly.GetCallingAssembly());
    private AdapterManager  _adapters { get; } = new(Assembly.GetCallingAssembly());

    protected Controller() {
        _commands.CommandEvent += CommandsOnCommandEvent;
        _adapters.AdapterEvent += AdaptersOnAdapterEvent;
    }

    public void Start() {
        OnControllerEvent(this, "Starting up the Controller");
        Logger.Log.Information("Starting Up the Controller. {0}", this.AttributeInfo()?.Name);
    }

    public void Stop() {
        OnControllerEvent(this,"Shutting Down the Controller");
        Logger.Log.Information("Shutting Down the Controller. {0}", this.AttributeInfo()?.Name);
    }

    /// <summary>
    ///     Execute a Given Command. We do this here so we can manage and log all command being executed
    ///     and the results that each command received.
    /// </summary>
    /// <param name="command">The command object to be executed</param>
    /// <returns>A resultOld object of type IResultOld which should be cast according to the command</returns>
    /// <exception cref="ApplicationException">Will throw an exception if no adapter specified</exception>
    public ICmdResult Execute(ICommand command) {
        if (!_commands.IsCommandSupported(command.GetType())) throw new ControllerException("Command is not supported.");
        if (_adapters.Adapter is null) throw new ControllerException("No Adapater has been attached. Cammand cannot execute");
        var result = command.Execute(_adapters.Adapter);
        result.Command = command;
        OnCommandExecute(this, command, result);
        return result;
    }

    public IAdapter? Adapter {
        get => _adapters.Adapter;
        set => _adapters.Adapter = value;
    }

    public bool IsCommandSupported<T>() where T : ICommand => _commands.IsCommandSupported<T>();
    public bool IsCommandSupported(Type command) => _commands.IsCommandSupported(command);
    public bool IsCommandSupported(string name) => _commands.IsCommandSupported(name);

    public bool IsAdapterSupported<T>() where T : IAdapter => _adapters.IsAdapterSupported<T>();
    public bool IsAdapterSupported(Type adapter) => _adapters.IsAdapterSupported(adapter);
    public bool IsAdapterSupported(string name) => _adapters.IsAdapterSupported(name);

    public List<AdapterAttribute> Adapters => _adapters.Adapters;
    public List<CommandAttribute> Commands => _commands.Commands;

    public IAdapter? CreateAdapter(string? name) => _adapters.Attach(name);
    public TCommand? CreateCommand<TCommand>() where TCommand : ICommand => (TCommand?)_commands.Create<TCommand>(Adapter!);
    public TCommand? CreateCommand<TCommand>(DCCAddress? address) where TCommand : ICommand => (TCommand?)_commands.Create<TCommand>(Adapter!,address);

    public abstract DCCAddress CreateAddress();
    public abstract DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    // Used for general Controller Events to be Raised
    private void OnControllerEvent(object? sender, string message ="") {
        ControllerEvent?.Invoke(sender, new ControllerEventArgs(message));
    }

    private void AdaptersOnAdapterEvent(object? sender, AdapterEventArgs e) {
        if (e.Adapter != null) {
            ControllerEvent?.Invoke(sender, new AdapterEventArgs(e.Adapter, e.AdapterEvent, e.Data, e.Message ?? ""));
        }
    }

    private void CommandsOnCommandEvent(object? sender, CommandEventArgs e) {
        if (e.Command != null) {
            ControllerEvent?.Invoke(sender, new CommandEventArgs(e.Command, e.Result, e.Message ?? ""));
        }
    }

    private void OnCommandExecute(Controller controller, ICommand command, ICmdResult result) {
        ControllerEvent?.Invoke(this, new CommandEventArgs(command,result, $"Command Executed on {controller.AttributeInfo().Name}"));
    }

}