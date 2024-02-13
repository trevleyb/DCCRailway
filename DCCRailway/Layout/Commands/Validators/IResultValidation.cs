using DCCRailway.System.Layout.Commands.Results;

namespace DCCRailway.System.Layout.Commands.Validators;

public interface IResultValidation {
    public ICommandResult Validate(byte[]? data);
}