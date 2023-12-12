namespace DCCRailway.System.Commands.Result;

public interface IResultStatus : IResult {
    public string Version { get; }
}