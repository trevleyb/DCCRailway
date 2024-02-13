using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Commands.Validators;

public interface IResultValidation {
    public ICommandResult Validate(byte[]? data);
}