namespace DCCRailway.System.Commands.Results;

public interface IResultStatus : IResult {
    public string Version { get; }
}