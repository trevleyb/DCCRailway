using DCCRailway.System.Utilities.Results;

namespace DCCRailway.System.Commands.Results;

public class CommandResult : Result {

    public List<CommandResultDataSet> DataSets { get; } = new List<CommandResultDataSet>();
    public List<CommandResultMessage> Messages { get; } = new List<CommandResultMessage>();

    private CommandResult(bool success, string error) : base(success, error) { }
    private CommandResult(bool success, string error, CommandResultDataSet dataSet) : base(success, error) {
        DataSets.Add(dataSet);
    }
    private CommandResult(bool success, string error, CommandResultMessage message) : base(success, error) {
        Messages.Add(message);
    }
    private CommandResult(bool success, string error, CommandResultDataSet dataSet, CommandResultMessage message) : base(success, error) {
        DataSets.Add(dataSet);
        Messages.Add(message);
    }

    public new static CommandResult Ok() {
        return new CommandResult(true, string.Empty);
    }
    public static CommandResult Ok(CommandResultDataSet dataSet) {
        return new CommandResult(true, string.Empty, dataSet);
    }
    public static CommandResult Ok(CommandResultMessage message) {
        return new CommandResult(true, string.Empty, message);
    }
    public static CommandResult Ok(CommandResultDataSet dataSet, CommandResultMessage message) {
        return new CommandResult(true, string.Empty, dataSet, message);
    }
    
    public new static CommandResult Fail(string message) {
        return new CommandResult(false, message);
    }
    public static CommandResult Fail(string message, CommandResultDataSet dataSet) {
        return new CommandResult(false, message,dataSet);
    }
    public static CommandResult Fail(string message, CommandResultMessage cmdMessage) {
        return new CommandResult(false, message, cmdMessage);
    }
    public static CommandResult Fail(string message, CommandResultDataSet dataSet, CommandResultMessage cmdMessage) {
        return new CommandResult(false, message,dataSet,cmdMessage);
    }
    
    public CommandResult Add(CommandResultDataSet dataSet) {
        DataSets.Add(dataSet);
        return this;
    }
    public CommandResult Add(CommandResultMessage message) {
        Messages.Add(message);
        return this;
    }
    
    public string? DataAsString => DataSets.Select(x => x.DataAsString).Aggregate((x, y) => $"{x}, {y}");
    
    
}
