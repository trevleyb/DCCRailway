namespace DCCRailway.System.Utilities.Results;

public interface IResult {
        public bool IsSuccess { get; }
        public bool IsFailure { get; }
        public bool IsOK      { get; }
        public string? Error   { get; }
}