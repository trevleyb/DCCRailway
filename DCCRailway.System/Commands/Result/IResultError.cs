namespace DCCRailway.System.Commands.Result;

public interface IResultError : IResult {
    public string Error { get; }
}