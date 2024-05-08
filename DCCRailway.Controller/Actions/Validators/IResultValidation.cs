using DCCRailway.Controller.Actions.Results;

namespace DCCRailway.Controller.Actions.Validators;

public interface IResultValidation {
    public ICmdResult Validate(byte[]? data);
}