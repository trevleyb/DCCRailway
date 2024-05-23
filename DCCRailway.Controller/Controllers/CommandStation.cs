using System.Reflection;
using DCCRailway.Common.Parameters;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Tasks;
using Serilog;

namespace DCCRailway.Controller.Controllers;

public abstract class CommandStation : ICommandStation, IParameterMappable {
    protected CommandStation(ILogger logger) {
        Logger                      =  logger;
        CommandManager              =  new CommandManager(logger, this, Assembly.GetCallingAssembly());
        AdapterManager              =  new AdapterManager(logger, this, Assembly.GetCallingAssembly());
        TaskManager                 =  new TaskManager(logger, this, Assembly.GetCallingAssembly());
        CommandManager.CommandEvent += CommandManagerOnCommandManagerEvent;
        AdapterManager.AdapterEvent += AdapterManagerOnAdapterManagerEvent;
    }

    private ILogger                                Logger         { get; init; }
    private TaskManager                            TaskManager    { get; } // Manages background Tasks
    private CommandManager                         CommandManager { get; } // Manages what commands are available
    private AdapterManager                         AdapterManager { get; } // Manages the attached Adapter(s)
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    public virtual void Start() {
        OnControllerEvent(this, "Starting up the CommandStation");
        Logger.Information("Starting Up the CommandStation. {0}", this.AttributeInfo()?.Name);
    }

    public virtual void Stop() {
        OnControllerEvent(this, "Shutting Down the CommandStation");
        Logger.Information("Shutting Down the CommandStation. {0}", this.AttributeInfo()?.Name);
    }

    public IAdapter? Adapter {
        get => AdapterManager.Adapter;
        set => AdapterManager.Adapter = value;
    }

    public bool IsCommandSupported<T>() where T : ICommand {
        return CommandManager.IsCommandSupported<T>();
    }

    public bool IsCommandSupported(Type command) {
        return CommandManager.IsCommandSupported(command);
    }

    public bool IsCommandSupported(string name) {
        return CommandManager.IsCommandSupported(name);
    }

    public bool IsAdapterSupported<T>() where T : IAdapter {
        return AdapterManager.IsAdapterSupported<T>();
    }

    public bool IsAdapterSupported(Type adapter) {
        return AdapterManager.IsAdapterSupported(adapter);
    }

    public bool IsAdapterSupported(string name) {
        return AdapterManager.IsAdapterSupported(name);
    }

    public List<AdapterAttribute> Adapters => AdapterManager.Adapters;
    public List<CommandAttribute> Commands => CommandManager.Commands;

    public IAdapter? CreateAdapter(string? name) {
        return AdapterManager.Attach(name);
    }

    public TCommand? CreateCommand<TCommand>() where TCommand : ICommand {
        return (TCommand?)CommandManager.Create<TCommand>(Adapter!);
    }

    public TCommand? CreateCommand<TCommand>(DCCAddress? address) where TCommand : ICommand {
        return (TCommand?)CommandManager.Create<TCommand>(Adapter!, address);
    }

    public List<TaskAttribute> Tasks => TaskManager.Tasks;

    public IControllerTask? CreateTask(string taskType) {
        return TaskManager.Create(taskType);
    }

    public IControllerTask? CreateTask(string name, string taskType, TimeSpan? frequency = null) {
        return TaskManager.Create(name, taskType, frequency);
    }

    public IControllerTask? CreateTask(string name, IControllerTask task, TimeSpan? frequency = null) {
        return TaskManager.Create(name, task, frequency);
    }

    public void StartAllTasks() {
        TaskManager.StartAllTasks();
    }

    public void StopAllTasks() {
        TaskManager.StopAllTasks();
    }

    public abstract DCCAddress CreateAddress();
    public abstract DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    public void OnCommandExecute(ICommandStation commandStation, ICommand command, ICmdResult result) {
        ControllerEvent?.Invoke(
            this, new CommandEventArgs(command, result, $"Command Executed on {commandStation.AttributeInfo().Name}"));
    }

    public IAdapter? AttachAdapter(IAdapter adapter) {
        return AdapterManager.Attach(adapter);
    }

    // Used for general CommandStation Events to be Raised
    public void OnControllerEvent(object? sender, string message = "") {
        ControllerEvent?.Invoke(sender, new ControllerEventArgs(message));
    }

    public void AdapterManagerOnAdapterManagerEvent(object? sender, AdapterEventArgs e) {
        if (e.Adapter != null)
            ControllerEvent?.Invoke(sender, new AdapterEventArgs(e.Adapter, e.AdapterEvent, e.Data, e.Message ?? ""));
    }

    public void CommandManagerOnCommandManagerEvent(object? sender, CommandEventArgs e) {
        if (e.Command != null)
            ControllerEvent?.Invoke(sender, new CommandEventArgs(e.Command, e.Result, e.Message ?? ""));
    }
}