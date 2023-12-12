using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Commands.Validator;

public interface IResultValidation {
    public CommandResult Validate(byte[] data);
}