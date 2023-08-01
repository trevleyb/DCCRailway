namespace DCCRailway.System.Commands.Results;

public class ResultError : ResultBase, IResultError, IResult {
    public ResultError(string description) : base(Array.Empty<byte>()) {
        Error = description;
    }

    public ResultError(string description, byte[] data) : base(data) {
        Error = description;
    }

    public string Error { get; }

    public new bool OK => false;
}