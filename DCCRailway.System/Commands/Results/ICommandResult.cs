namespace DCCRailway.System.Commands.Results;

public interface ICommandResult {
        public bool    IsSuccess { get; }
        public bool    IsFailure { get; }
        public bool    IsOK      { get; }
        public string? Error     { get; }
        public byte    Byte      { get; }
        public CommandResultData Data { get; }
}