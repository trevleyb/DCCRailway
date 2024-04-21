using DCCRailway.Station.Commands.Results;

namespace DCCRailway.Station.Commands.Validators;

public interface IResultValidation {
    public ICommandResult Validate(byte[]? data);
}