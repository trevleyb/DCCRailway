using System.Diagnostics;
using System.Reflection;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Exceptions;
using Serilog;

namespace DCCRailway.Controller.Controllers;

public class CommandManager(ILogger logger, ICommandStation commandStation, Assembly assembly) {
    private Dictionary<Type, (CommandAttribute Attributes, Type ConcreteType)> _commands = [];
    private Assembly                                                           _assembly { get; } = assembly;

    public List<CommandAttribute> Commands {
        get {
            if (_commands.Any() is false) RegisterCommands();

            // ToDo: Add in here if the command is supported.
            return _commands.Values.Select(x => x.Item1).ToList();
        }
    }

    public event EventHandler<CommandEventArgs> CommandEvent;

    private void RegisterCommands() {
        if (_assembly is null) throw new ApplicationException("No Assembly has been set for the Command Manager");

        foreach (var command in _assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(ICommand)))) {
            var attr = AttributeExtractor.GetAttribute<CommandAttribute>(command);

            if (attr != null && !string.IsNullOrEmpty(attr.Name)) {
                var commandInterface = command.ImplementedInterfaces.First(x => x.FullName != null && x.FullName.StartsWith("DCCRailway.Controller.Actions.Commands.I", StringComparison.InvariantCultureIgnoreCase)) ?? null;

                if (commandInterface is not null)
                    if (!_commands.ContainsKey(commandInterface))
                        _commands.TryAdd(commandInterface, (attr, command));
            }
        }
    }

    protected void RegisterCommand<TInterface, TConcrete>() where TInterface : ICommand where TConcrete : ICommand {
        var attr = AttributeExtractor.GetAttribute<CommandAttribute>(typeof(TInterface));

        if (attr == null || string.IsNullOrEmpty(attr.Name))
            throw new ApplicationException("Command does not contain AttributeInfo Definition. Add AttributeInfo first");
        if (!_commands.ContainsKey(typeof(TInterface))) _commands.TryAdd(typeof(TInterface), (attr, typeof(TConcrete)));
    }

    protected void ClearCommands() {
        _commands = [];
    }

    private ICommand? AttachProperties(ICommand? command, DCCAddress? address = null) {
        if (command != null) {
            if (address is not null && command is ICmdAddress cmdAddress) cmdAddress.Address = address;
            command.Adapter        = commandStation.Adapter ?? throw new ControllerException("Adapter is not defined.");
            command.CommandStation = commandStation;
            command.Logger         = logger; // Should probably be injected in the constructor..
        }

        return command;
    }

    public ICommand? Create(string name, IAdapter adapter, DCCAddress? address = null) {
        var command = Create(name);
        return AttachProperties(command, address);
    }

    public ICommand? Create<TCommand>(IAdapter adapter, DCCAddress? address = null) where TCommand : ICommand {
        var command = Create<TCommand>();
        return AttachProperties(command, address);
    }

    public ICommand? Create(string name, DCCAddress? address = null) {
        if (_commands.Any() is false) RegisterCommands();

        var entry = _commands.FirstOrDefault(x => {
            Debug.Assert(x.Value.Attributes.Name != null, "x.Value.Attributes.Name != null");
            return x.Value.Attributes.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        });

        if (entry.Equals(default(KeyValuePair<Type, (CommandAttribute, Type)>)) && entry.Key != null) {
            var command = CreateInstance(entry.Value.ConcreteType);
            return AttachProperties(command, address);
        }

        throw new ApplicationException("Selected Command Type is not supported by this Adapter.");
    }

    public ICommand? Create<TCommand>(DCCAddress? address = null) where TCommand : ICommand {
        if (_commands.Any() is false) RegisterCommands();

        if (IsCommandSupported<TCommand>()) {
            var typeToCreate = _commands[typeof(TCommand)].ConcreteType ?? null;

            if (typeToCreate is null)
                throw new ApplicationException("Selected Command Type is not supported by this Adapter.");
            var command = CreateInstance(typeToCreate);
            return AttachProperties(command, address);
        }

        throw new ApplicationException("Selected Command Type is not supported by this Adapter.");
    }

    private ICommand? CreateInstance(Type typeToCreate) {
        try {
            var command = (ICommand?)Activator.CreateInstance(typeToCreate);
            if (command == null) throw new ApplicationException("Could not create an instance of the command.");
            return command;
        } catch (Exception ex) {
            throw new ApplicationException("Could not create an instance of the command.", ex);
        }
    }

    public bool IsCommandSupported<T>() where T : ICommand {
        return _commands.ContainsKey(typeof(T));
    }

    public bool IsCommandSupported(Type command) {
        return command.GetInterfaces().Any(iface => _commands.ContainsKey(iface));
    }

    public bool IsCommandSupported(string name) {
        return _commands.Any(item => item.Key.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) || item.Value.ConcreteType.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    #region Raise Events
    // Raise when this CommandStation executes a command
    private void OnCommandExecute(object sender, ICommand command, ICmdResult result) {
        var e = new CommandEventArgs(command, result, $"Command {command.GetType().Name} executed with resultOld {result.GetType().Name}");
        CommandEvent?.Invoke(sender, e);
    }
    #endregion
}