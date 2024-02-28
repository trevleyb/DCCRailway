using DCCRailway.DCCController.Commands.Results;

namespace DCCRailway.DCCController.Commands.Validators;

public interface IResultValidation {
    public ICommandResult Validate(byte[]? data);
}