using DCCRailway.CmdStation.Commands.Results;

namespace DCCRailway.CmdStation.Commands.Validators;

public interface IResultValidation {
    public ICmdResult Validate(byte[]? data);
}