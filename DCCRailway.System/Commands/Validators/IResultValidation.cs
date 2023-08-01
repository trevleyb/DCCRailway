using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Commands.Validators;

public interface IResultValidation {
    public IResult Validate(byte[] data);
}