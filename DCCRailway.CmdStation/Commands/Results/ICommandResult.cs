namespace DCCRailway.CmdStation.Commands.Results;

public interface ICommandResult {
    public bool               IsOK      { get; }
    public string?            Error     { get; }
    public byte               Byte      { get; }
    public CommandResultData? Data      { get; }
}