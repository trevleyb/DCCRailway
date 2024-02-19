using DCCRailway.Layout.Commands.Results;

namespace DCCRailway.Layout.Commands.Validators;

public interface IResultValidation {
    public ICommandResult Validate(byte[]? data);
}