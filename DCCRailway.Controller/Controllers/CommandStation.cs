using System.Reflection;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Controller.Tasks;

namespace DCCRailway.Controller.Controllers;

public abstract class CommandStation : ICommandStation, IParameterMappable {
    public event EventHandler<ControllerEventArgs> ControllerEvent;

    private TaskManager    TaskManager    { get; } // Manages background Tasks
    private CommandManager CommandManager { get; } // Manages what commands are available
    private AdapterManager AdapterManager { get; } // Manages the attached Adapter(s)

    protected CommandStation() {
        CommandManager = new CommandManager(this, Assembly.GetCallingAssembly());
        AdapterManager = new AdapterManager(this, Assembly.GetCallingAssembly());
        TaskManager    = new TaskManager(this, Assembly.GetCallingAssembly());

        CommandManager.CommandEvent += CommandManagerOnCommandManagerEvent;
        AdapterManager.AdapterEvent += AdapterManagerOnAdapterManagerEvent;
    }

    public virtual void Start() {
        OnControllerEvent(this, "Starting up the CommandStation");
        Logger.Log.Information("Starting Up the CommandStation. {0}", this.AttributeInfo()?.Name);
    }

    public virtual void Stop() {
        OnControllerEvent(this, "Shutting Down the CommandStation");
        Logger.Log.Information("Shutting Down the CommandStation. {0}", this.AttributeInfo()?.Name);
    }

    public IAdapter? Adapter {
        get => AdapterManager.Adapter;
        set => AdapterManager.Adapter = value;
    }

    public bool IsCommandSupported<T>() where T : ICommand => CommandManager.IsCommandSupported<T>();
    public bool IsCommandSupported(Type command)           => CommandManager.IsCommandSupported(command);
    public bool IsCommandSupported(string name)            => CommandManager.IsCommandSupported(name);

    public bool IsAdapterSupported<T>() where T : IAdapter => AdapterManager.IsAdapterSupported<T>();
    public bool IsAdapterSupported(Type adapter)           => AdapterManager.IsAdapterSupported(adapter);
    public bool IsAdapterSupported(string name)            => AdapterManager.IsAdapterSupported(name);

    public List<AdapterAttribute> Adapters => AdapterManager.Adapters;
    public List<CommandAttribute> Commands => CommandManager.Commands;

    public IAdapter? CreateAdapter(string? name)                                            => AdapterManager.Attach(name);
    public TCommand? CreateCommand<TCommand>() where TCommand : ICommand                    => (TCommand?)CommandManager.Create<TCommand>(Adapter!);
    public TCommand? CreateCommand<TCommand>(DCCAddress? address) where TCommand : ICommand => (TCommand?)CommandManager.Create<TCommand>(Adapter!, address);

    public List<TaskAttribute> Tasks                                                                     => TaskManager.Tasks;
    public IControllerTask?    CreateTask(string taskType)                                               => TaskManager.Create(taskType);
    public IControllerTask?    AttachTask(IControllerTask task)                                          => TaskManager.Attach(task);
    public IControllerTask?    AttachTask(string name, string taskType, TimeSpan? frequency = null)      => TaskManager.Attach(name, taskType, frequency);
    public IControllerTask?    AttachTask(string name, IControllerTask task, TimeSpan? frequency = null) => TaskManager.Attach(name, task, frequency);
    public void                StartAllTasks()                                                           => TaskManager.StartAllTasks();
    public void                StopAllTasks()                                                            => TaskManager.StopAllTasks();

    public abstract DCCAddress CreateAddress();
    public abstract DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    // Used for general CommandStation Events to be Raised
    public void OnControllerEvent(object? sender, string message = "") {
        ControllerEvent?.Invoke(sender, new ControllerEventArgs(message));
    }

    public void AdapterManagerOnAdapterManagerEvent(object? sender, AdapterEventArgs e) {
        if (e.Adapter != null) ControllerEvent?.Invoke(sender, new AdapterEventArgs(e.Adapter, e.AdapterEvent, e.Data, e.Message ?? ""));
    }

    public void CommandManagerOnCommandManagerEvent(object? sender, CommandEventArgs e) {
        if (e.Command != null) ControllerEvent?.Invoke(sender, new CommandEventArgs(e.Command, e.Result, e.Message ?? ""));
    }

    public void OnCommandExecute(ICommandStation commandStation, ICommand command, ICmdResult result) {
        ControllerEvent?.Invoke(this, new CommandEventArgs(command, result, $"Command Executed on {commandStation.AttributeInfo().Name}"));
    }
}