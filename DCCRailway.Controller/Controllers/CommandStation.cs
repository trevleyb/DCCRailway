using System.Reflection;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Controller.Helpers;

namespace DCCRailway.Controller.Controllers;

public abstract class CommandStation : ICommandStation, IParameterMappable {

    public event EventHandler<ControllerEventArgs> ControllerEvent;

    private CommandManager  _commandManager { get; }
    private AdapterManager  _adapterManager { get; }

    protected CommandStation() {
        _commandManager = new(this,Assembly.GetCallingAssembly());
        _adapterManager = new(this, Assembly.GetCallingAssembly());

        _commandManager.CommandEvent += CommandManagerOnCommandManagerEvent;
        _adapterManager.AdapterEvent += AdapterManagerOnAdapterManagerEvent;
    }

    public void Start() {
        OnControllerEvent(this, "Starting up the CommandStation");
        Logger.Log.Information("Starting Up the CommandStation. {0}", this.AttributeInfo()?.Name);
    }

    public void Stop() {
        OnControllerEvent(this,"Shutting Down the CommandStation");
        Logger.Log.Information("Shutting Down the CommandStation. {0}", this.AttributeInfo()?.Name);
    }

    public IAdapter? Adapter {
        get => _adapterManager.Adapter;
        set => _adapterManager.Adapter = value;
    }

    public bool IsCommandSupported<T>() where T : ICommand => _commandManager.IsCommandSupported<T>();
    public bool IsCommandSupported(Type command) => _commandManager.IsCommandSupported(command);
    public bool IsCommandSupported(string name) => _commandManager.IsCommandSupported(name);

    public bool IsAdapterSupported<T>() where T : IAdapter => _adapterManager.IsAdapterSupported<T>();
    public bool IsAdapterSupported(Type adapter) => _adapterManager.IsAdapterSupported(adapter);
    public bool IsAdapterSupported(string name) => _adapterManager.IsAdapterSupported(name);

    public List<AdapterAttribute> Adapters => _adapterManager.Adapters;
    public List<CommandAttribute> Commands => _commandManager.Commands;

    public IAdapter? CreateAdapter(string? name) => _adapterManager.Attach(name);
    public TCommand? CreateCommand<TCommand>() where TCommand : ICommand => (TCommand?)_commandManager.Create<TCommand>(Adapter!);
    public TCommand? CreateCommand<TCommand>(DCCAddress? address) where TCommand : ICommand => (TCommand?)_commandManager.Create<TCommand>(Adapter!,address);

    public abstract DCCAddress CreateAddress();
    public abstract DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    // Used for general CommandStation Events to be Raised
    public void OnControllerEvent(object? sender, string message ="") {
        ControllerEvent?.Invoke(sender, new ControllerEventArgs(message));
    }

    public void AdapterManagerOnAdapterManagerEvent(object? sender, AdapterEventArgs e) {
        if (e.Adapter != null) {
            ControllerEvent?.Invoke(sender, new AdapterEventArgs(e.Adapter, e.AdapterEvent, e.Data, e.Message ?? ""));
        }
    }

    public void CommandManagerOnCommandManagerEvent(object? sender, CommandEventArgs e) {
        if (e.Command != null) {
            ControllerEvent?.Invoke(sender, new CommandEventArgs(e.Command, e.Result, e.Message ?? ""));
        }
    }

    public void OnCommandExecute(ICommandStation commandStation, ICommand command, ICmdResult result) {
        ControllerEvent?.Invoke(this, new CommandEventArgs(command,result, $"Command Executed on {commandStation.AttributeInfo().Name}"));
    }

}