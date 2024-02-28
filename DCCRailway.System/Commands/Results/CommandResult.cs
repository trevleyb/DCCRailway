using DCCRailway.Common.Utilities;
using DCCRailway.Common.Utilities.Results;

namespace DCCRailway.System.Commands.Results;

public class CommandResult : Result<CommandResultData>, ICommandResult, IResult {

    protected CommandResult(bool isSuccess, CommandResultData? value, string? error) : base(isSuccess, value, error) { }

    public new static CommandResult Success() {
        return new CommandResult (true, null, null );
    }
    public new static CommandResult Success(CommandResultData? value) {
        return new CommandResult(true, value, null );
    }
    public static CommandResult Success(byte[] data) {
        return new CommandResult(true, new CommandResultData(data), null );
    }

    public new static CommandResult Fail(string? error) {
        return new CommandResult(false, null, error);
    }
    public new static CommandResult Fail(string? error, CommandResultData? value) {
        return new CommandResult(false, value, error);
    }
    public static CommandResult Fail(string? error, byte[] data) {
        return new CommandResult(false, new CommandResultData(data), error);
    }

    public byte Byte => Data?.Data?[0] ?? 0;
    public CommandResultData? Data => Value;
    public new string? ToString() => Data?.Data.ToDisplayValues() ?? "";
}