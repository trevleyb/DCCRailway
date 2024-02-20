using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Adapters.Events;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Controllers.Events;
using DCCRailway.Layout.SystemEvents;
using DCCRailway.Layout.Types;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.Controllers;

public abstract class Controller : IController {
    private IAdapter? _adapter;                                       // Stores the adapter to be used
    private Dictionary<Type, (Type Adapter, string Name)> _adapters = []; // Stores what operations the controller will provide
    private Dictionary<Type, (Type Command, string Name)> _commands = []; // Stores what operations the controller will provide

    public event EventHandler<ControllerEventArgs> ControllerEvent;
    
    protected Controller() { }
    
    /// <summary>
    ///     Execute a Given Command. We do this here so we can manage and log all command being executed
    ///     and the results that each command received.
    /// </summary>
    /// <param name="command">The command object to be executed</param>
    /// <returns>A resultOld object of type IResultOld which should be cast according to the command</returns>
    /// <exception cref="ApplicationException">Will throw an exception if no adapter specified</exception>
    public ICommandResult Execute(ICommand command) {
        if (_adapter == null) throw new ApplicationException("No Adapter has been provided.");
        var result = command.Execute(_adapter);
        OnCommandExecute(this, command, result);
        return result;
    }

    #region Create an Address. Must be overriden to the supported Command Station
    public abstract IDCCAddress CreateAddress();
    public abstract IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);
    #endregion

    #region Create and attach an Adapter to this controller
    /// <summary>
    ///     Property for the Adapter that this controller will communicate through
    /// </summary>
    public IAdapter? Adapter {
        get => _adapter;
        set {
            if (_adapter != value) Detach();
            Attach(value);
        }
    }
    
    /// <summary>
    ///     Create and return an Adapter. This does not attach it. This command should be executed as
    ///     Controller.Adapter = Controller.CreateAdapter(name);
    /// </summary>
    public IAdapter? CreateAdapter(string adapterName) {
        if (_adapter is null) RegisterAdapters();
        if (SupportedAdapters is not { Count: > 0 }) return null;
        foreach (var (adapter, name) in SupportedAdapters.Values!) {
            if (name.Equals(adapterName, StringComparison.InvariantCultureIgnoreCase)) {
                return (IAdapter?)Activator.CreateInstance(adapter);
            }
        }
        return null;
    }

    /// <summary>
    ///     Remove an previously attached adapter. Ensure it is closed and resources returned
    /// </summary>
    private void Detach() {
        if (_adapter != null) {
            OnAdapterRemoved(this,_adapter);
            _adapter.Disconnect();
            _adapter = null;
            _commands = new Dictionary<Type, (Type command, string name)>();
        }
    }

    private void Attach(IAdapter? adapter) {
        if (adapter != null) {
            _adapter               =  adapter;
            _adapter.DataReceived  += (sender, e) => OnAdapterEvent(sender!, _adapter, e);
            _adapter.DataSent      += (sender, e) => OnAdapterEvent(sender!, _adapter, e);
            _adapter.ErrorOccurred += (sender, e)    => OnAdapterEvent(sender!, _adapter, e);
            _adapter.Connect();
            RegisterCommands();
            OnAdapterAdd(this,adapter);
        }
    }
    #endregion

    #region Generic Factory Implementation to register what operations each Controller will provide
    /// <summary>
    ///     This function must be overridden in the derived class to ensure that the operations
    ///     that this controller provides are loaded. This is done because some operations may only
    ///     be supported on a particular adapter or interface (eg: NCE does not support the clock
    ///     functions if using the USBSerial adapter).
    /// </summary>

    private Dictionary<Type, (Type Command, string Name)> SupportedCommands {
        get {
            if (_commands.Count == 0) RegisterCommands();
            return _commands;
        }
    }

    private Dictionary<Type, (Type Adapter, string Name)> SupportedAdapters {
        get { 
            if (_adapters.Count == 0) RegisterAdapters();
            return _adapters;
        }
    }

    public List<(Type Command, string Name)> Commands => SupportedCommands.Values.ToList();
    public List<(Type Adapter, string Name)> Adapters => SupportedAdapters.Values.ToList();
    
    protected abstract void RegisterCommands();
    protected abstract void RegisterAdapters();
    
    public bool IsCommandSupported<T>() where T : ICommand => SupportedCommands.ContainsKey(typeof(T));
    public bool IsAdapterSupported<T>() where T : IAdapter => SupportedAdapters.ContainsKey(typeof(T));
    public bool IsCommandSupported(string name) => SupportedCommands.Any(pair => pair.Value.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    public bool IsAdapterSupported(string name) => SupportedAdapters.Any(pair => pair.Value.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

    protected void RegisterCommand<T>(Type command) where T : ICommand {
        var attr = AttributeExtractor.GetAttribute<CommandAttribute>(command);
        if (attr == null || string.IsNullOrEmpty(attr.Name)) throw new ApplicationException("Command does not contain AttributeInfo Definition. Add AttributeInfo first");
        if (!_commands.ContainsKey(typeof(T))) _commands.TryAdd(typeof(T), (command, attr.Name));
    }

    protected void RegisterAdapter<T>() where T : IAdapter {
        var attr = AttributeExtractor.GetAttribute<AdapterAttribute>(typeof(T));
        if (attr == null || string.IsNullOrEmpty(attr.Name)) throw new ApplicationException("Adapter instance cannot be NULL and must be a concrete object.");
        if (!_adapters.ContainsKey(typeof(T))) _adapters.TryAdd(typeof(T), (typeof(T), attr.Name));
    }

    protected void ClearAdapters() {
        _adapters = new Dictionary<Type, (Type Adapter, string Name)>();
    }

    protected void ClearCommands() {
        _commands = new Dictionary<Type, (Type Adapter, string Name)>();
    }
    #endregion

    #region Create Command Objects that are supported by this Controller.
    /// <summary>
    ///     Creates a command object that can be executed. A command object can be
    ///     executed by calling its execute function or passing it back to the
    ///     controller to ask it to execute it.
    /// </summary>
    /// <typeparam name="T">An interface that adheres to a ICommand interface</typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <returns>An object instance that is a Types T object</returns>
    public TCommand? CreateCommand<TCommand>() where TCommand : ICommand {
        if (_adapter == null) throw new ApplicationException("Adapter cannot be null when creating commands");
        var typeToCreate = _commands[typeof(TCommand)].Command ?? null;
        if (typeToCreate == null) throw new ApplicationException("Should not have an instance where the command returned is NULL");

        try {
            var command = (TCommand?)Activator.CreateInstance(typeToCreate, true);
            if (command == null) throw new ApplicationException("Could not create an instance of the command.");
            return command;
        } catch (Exception ex) {
            throw new ApplicationException("Could not create an instance of the command.", ex);
        }
    }

    public TCommand? CreateCommand<TCommand>(int value) where TCommand : ICommand => Create<TCommand, int>(value);
    public TCommand? CreateCommand<TCommand>(byte value) where TCommand : ICommand => Create<TCommand, byte>(value);
    private TCommand? Create<TCommand, TP>(TP value) where TCommand : ICommand {
        if (_adapter == null) throw new ApplicationException("Adapter cannot be null when creating commands");
        var typeToCreate = _commands[typeof(TCommand)].Command ?? null;
        if (typeToCreate == null) throw new ApplicationException("Should not have an instance where the command returned is NULL");

        try {
            var command = (TCommand?)Activator.CreateInstance(typeToCreate, value);
            if (command == null) throw new ApplicationException("Could not create an instance of the command.");
            return command;
        } catch (Exception ex) {
            throw new ApplicationException("Could not create an instance of the command.", ex);
        }
    }
    #endregion

    #region Raise Events
    // Raise when this Controller executes a command
    private void OnCommandExecute(object sender, ICommand command, ICommandResult result) {
        var e = new ControllerEventCommandExec(command, result, Adapter!, $"Command {command.GetType().Name} executed with resultOld {result.GetType().Name}");
        ControllerEvent?.Invoke(sender, e);
    }

    // Raise when we add an Adapter to this controller
    private void OnAdapterAdd(object sender, IAdapter adapter) {
        var e = new ControllerEventAdapterAdd(adapter, $"Adapter {adapter.GetType().Name} added");
        ControllerEvent?.Invoke(sender, e);
    }

    // Raise when we delete or remove an Adapter from this Controller
    private void OnAdapterRemoved(object sender, IAdapter adapter) {
        var e = new ControllerEventAdapterDel(adapter, $"Adapter {adapter.GetType().Name} removed");
        ControllerEvent?.Invoke(sender, e);
    }

    // Raise when we delete or remove an Adapter from this Controller
    private void OnAdapterEvent(object sender, IAdapter adapter, IAdapterEvent adapterEvent) {
        var e = new ControllerEventAdapter(adapter, adapterEvent ,$"Adapter {adapter.GetType().Name} removed");
        ControllerEvent?.Invoke(sender, e);
    }

    // Used for general Controller Events to be Raised
    private void OnControllerEvent(object sender, string message) {
        var e = new ControllerEventArgs(message);
        ControllerEvent?.Invoke(sender, e);
    }
    #endregion
}