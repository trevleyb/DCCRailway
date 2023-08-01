namespace DCCRailway.System.Commands.Results;

public interface IResultError : IResult {
    public string Error { get; }
}