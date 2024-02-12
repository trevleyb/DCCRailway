using DCCRailway.System.Commands.Results;
using DCCRailway.System.Utilities.Results;

namespace DCCRailway.System.Commands.Validator;

public interface IResultValidation {
    public ICommandResult Validate(byte[]? data);
}