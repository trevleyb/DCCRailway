using DCCRailway.System.Commands.Result;

namespace DCCRailway.System.Commands.Validator;

public interface IResultValidation {
    public IResult Validate(byte[] data);
}