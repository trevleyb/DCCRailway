using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Controllers.Events;

namespace DCCRailway.System.Controllers;

public class CommandManager {

    private Dictionary<Type, (CommandAttribute Attributes, Type ConcreteType)>  _commands = [];
    public event EventHandler<CommandEventArgs> CommandEvent;

    public bool IsCommandSupported<T>() where T : ICommand => _commands.ContainsKey(typeof(T));
    public bool IsCommandSupported(Type command) => _commands.ContainsKey(command);
    public bool                   IsCommandSupported(string name)            => _commands.Any(pair => pair.Value.Attributes.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    public List<CommandAttribute> Commands {
        get {
            if (_commands.Any() is false) RegisterCommands();
            return _commands.Values.Select(x => x.Item1) .ToList();
        }
    }

    private void RegisterCommands() {}

    protected void RegisterCommand<TInterface,TConcrete>() where TInterface : ICommand where TConcrete : ICommand {
        var attr = AttributeExtractor.GetAttribute<CommandAttribute>(typeof(TInterface));
        if (attr == null || string.IsNullOrEmpty(attr.Name))
            throw new ApplicationException("Command does not contain AttributeInfo Definition. Add AttributeInfo first");
        if (!_commands.ContainsKey(typeof(TInterface))) _commands.TryAdd(typeof(TInterface), (attr,typeof(TConcrete)));
    }

    protected void ClearCommands() {
        _commands = [];
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

    #region Raise Events
    // Raise when this Controller executes a command
    private void OnCommandExecute(object sender, ICommand command, ICommandResult result) {
        var e = new CommandEventArgs(command, result, $"Command {command.GetType().Name} executed with resultOld {result.GetType().Name}");
        CommandEvent?.Invoke(sender, e);
    }
    #endregion
}