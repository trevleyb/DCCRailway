namespace DCCRailway.Core.Systems.Commands.Results; 

public interface IResultStatus : IResult {
    public string Version { get; }
}