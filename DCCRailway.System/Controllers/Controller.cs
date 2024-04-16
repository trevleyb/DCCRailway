using System.Reflection;
using DCCRailway.Common.Types;
using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Controllers.Events;
using DCCRailway.System.Exceptions;

namespace DCCRailway.System.Controllers;

public abstract class Controller : IController {

    public event EventHandler<ControllerEventArgs> ControllerEvent;

    private CommandManager _commands { get; } = new(Assembly.GetCallingAssembly());
    private AdapterManager _adapters { get; } = new(Assembly.GetCallingAssembly());

    protected Controller() {
        _commands.CommandEvent += CommandsOnCommandEvent;
        _adapters.AdapterEvent += AdaptersOnAdapterEvent;
    }

    /// <summary>
    ///     Execute a Given Command. We do this here so we can manage and log all command being executed
    ///     and the results that each command received.
    /// </summary>
    /// <param name="command">The command object to be executed</param>
    /// <returns>A resultOld object of type IResultOld which should be cast according to the command</returns>
    /// <exception cref="ApplicationException">Will throw an exception if no adapter specified</exception>
    public ICommandResult Execute(ICommand command) {
        if (!_commands.IsCommandSupported(command.GetType())) throw new ControllerException("Command is not supported.");
        if (_adapters.Adapter is null) throw new ControllerException("No Adapater has been attached. Cammand cannot execute");
        var result = command.Execute(_adapters.Adapter);
        OnCommandExecute(this, command, result);
        return result;
    }

    public List<AdapterAttribute> Adapters                                            => _adapters.Adapters;
    public IAdapter?              CreateAdapter(string name)                          => _adapters.Attach(name);
    public TCommand?              CreateCommand<TCommand>() where TCommand : ICommand => (TCommand?)_commands.Create<TCommand>();
    public List<CommandAttribute> Commands                                            => _commands.Commands;
    public IAdapter? Adapter {
        get => _adapters.Adapter;
        set => _adapters.Adapter = value;
    }

    public abstract IDCCAddress CreateAddress();
    public abstract IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long);

    // Used for general Controller Events to be Raised
    private void OnControllerEvent(object? sender, string message ="") {
        ControllerEvent?.Invoke(sender, new ControllerEventArgs(message));
    }

    private void AdaptersOnAdapterEvent(object? sender, AdapterEventArgs e) {
        ControllerEvent?.Invoke(sender, new ControllerEventArgs(null, e.Adapter, null, e, $"Adapater action={e.AdapterEvent}"));
    }

    private void CommandsOnCommandEvent(object? sender, CommandEventArgs e) {
        ControllerEvent?.Invoke(sender, new ControllerEventArgs(e.Command, e.Adapter, e.Result, e, $"Command={e.Command}"));
    }

    private void OnCommandExecute(Controller controller, ICommand command, ICommandResult result) {
        ControllerEvent?.Invoke(this, new ControllerEventArgs(command, _adapters.Adapter, result, null, "Command Executed"));
    }

}