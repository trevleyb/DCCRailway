using DCCRailway.CmdStation.Actions.Results;

namespace DCCRailway.CmdStation.Actions.Validators;

public interface IResultValidation {
    public ICmdResult Validate(byte[]? data);
}