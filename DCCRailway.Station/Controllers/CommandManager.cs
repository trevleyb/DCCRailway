using System.Reflection;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Controllers.Events;

namespace DCCRailway.Station.Controllers;

public class CommandManager(Assembly assembly) {

    private Dictionary<Type, (CommandAttribute Attributes, Type ConcreteType)> _commands = [];
    public event EventHandler<CommandEventArgs>                                CommandEvent;
    private Assembly                                                           _assembly { get; set; } = assembly;

    public List<CommandAttribute> Commands {
        get {
            if (_commands.Any() is false) RegisterCommands();
            // ToDo: Add in here if the command is supported.
            return _commands.Values.Select(x => x.Item1) .ToList();
        }
    }

    private void RegisterCommands() {
        if (_assembly is null) throw new ApplicationException("No Assembly has been set for the Command Manager");
        foreach (var command in _assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(ICommand)))) {
            var attr = AttributeExtractor.GetAttribute<CommandAttribute>(command);
            if (attr != null && !string.IsNullOrEmpty(attr.Name)) {
                var commandInterface = command.ImplementedInterfaces.First(x => x.FullName != null && x.FullName.StartsWith("DCCRailway.Station.Commands.Types.I", StringComparison.InvariantCultureIgnoreCase)) ?? null;
                if (commandInterface is not null) {
                    if (!_commands.ContainsKey(commandInterface)) _commands.TryAdd(commandInterface, (attr, command));
                }
            }
        }
    }

    protected void RegisterCommand<TInterface,TConcrete>() where TInterface : ICommand where TConcrete : ICommand {
        var attr = AttributeExtractor.GetAttribute<CommandAttribute>(typeof(TInterface));
        if (attr == null || string.IsNullOrEmpty(attr.Name))
            throw new ApplicationException("Command does not contain AttributeInfo Definition. Add AttributeInfo first");
        if (!_commands.ContainsKey(typeof(TInterface))) _commands.TryAdd(typeof(TInterface), (attr,typeof(TConcrete)));
    }

    protected void ClearCommands() {
        _commands = [];
    }

    public ICommand? Create(string name, IAdapter adapter) {
        var command = Create(name);
        if (command != null) command.Adapter = adapter;
        return command;
    }

    public ICommand? Create<TCommand>(IAdapter adapter)  where TCommand : ICommand {
        var command = Create<TCommand>();
        if (command != null) command.Adapter = adapter;
        return command;
    }

    public ICommand? Create(string name) {
        if (_commands.Any() is false) RegisterCommands();
        var entry = _commands.FirstOrDefault(x => x.Value.Attributes.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        if (entry.Equals(default(KeyValuePair<Type, (CommandAttribute, Type)>)) && entry.Key != null) {
            return CreateInstance(entry.Value.ConcreteType);
        }
        throw new ApplicationException("Selected Command Type is not supported by this Adapter.");
    }

    public ICommand? Create<TCommand>() where TCommand : ICommand {
        if (_commands.Any() is false) RegisterCommands();
        if (IsCommandSupported<TCommand>()) {
            var typeToCreate = _commands[typeof(TCommand)].ConcreteType ?? null;
            if (typeToCreate is null)
                throw new ApplicationException("Selected Command Type is not supported by this Adapter.");
            return CreateInstance(typeToCreate);
        }
        throw new ApplicationException("Selected Command Type is not supported by this Adapter.");
    }

    private static ICommand? CreateInstance(Type typeToCreate) {
        try {
            var command = (ICommand?)Activator.CreateInstance(typeToCreate);
            if (command == null) throw new ApplicationException("Could not create an instance of the command.");
            return command;
        }
        catch (Exception ex) {
            throw new ApplicationException("Could not create an instance of the command.", ex);
        }
    }

    public bool IsCommandSupported<T>() where T : ICommand => _commands.ContainsKey(typeof(T));
    public bool IsCommandSupported(Type command) => command.GetInterfaces().Any(iface => _commands.ContainsKey(iface));
    public bool IsCommandSupported(string name) => _commands.Any(item =>
            item.Key.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) ||
            item.Value.ConcreteType.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
            );

    #region Raise Events
    // Raise when this Controller executes a command
    private void OnCommandExecute(object sender, ICommand command, ICommandResult result) {
        var e = new CommandEventArgs(command, result, $"Command {command.GetType().Name} executed with resultOld {result.GetType().Name}");
        CommandEvent?.Invoke(sender, e);
    }
    #endregion
}