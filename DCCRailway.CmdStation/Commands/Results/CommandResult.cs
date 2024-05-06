using DCCRailway.Common.Helpers;
using DCCRailway.Common.Results;

namespace DCCRailway.CmdStation.Commands.Results;

public class CommandResult : Result<CommandResultData>, ICommandResult, IResult {

    protected CommandResult(bool isSuccess, CommandResultData? value = null, string? error = null) : base(isSuccess, value, error) { }

    public new static CommandResult Success()                         => new(true, null, null);
    public new static CommandResult Success(CommandResultData? value) => new(true, value, null);
    public static CommandResult Success(byte[] data) => new(true, new CommandResultData(data), null);
    public new static CommandResult Fail(string? error) => new(false, null, error);
    public new static CommandResult Fail(string? error, CommandResultData? value) => new(false, value, error);
    public static CommandResult Fail(string? error, byte[] data) => new(false, new CommandResultData(data), error);

    public     byte               Byte       => Data?.Data?[0] ?? 0;
    public     CommandResultData? Data       => Value;
    public new string?            ToString() => Data?.Data.ToDisplayValues() ?? "";
}