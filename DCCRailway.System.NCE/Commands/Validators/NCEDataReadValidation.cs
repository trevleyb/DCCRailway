using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;

namespace DCCRailway.System.NCE.Commands.Validators;

public class NCEDataReadValidation : IResultValidation {
    public IResult Validate(byte[] data) {
        return data.Length switch {
            0 => new ResultError("Unexpected data returned and not processed. Expected 2 Bytes.", data!),
            1 => data[0] switch {
                (byte)'0' => new ResultError("Command not supported or not in Programming Track mode."),
                (byte)'3' => new ResultError("Data provided is out of range."),
                _ => new ResultError("Unknown response from the NCE System.", data!)
            },
            2 => data[1] switch {
                (byte)'0' => new ResultError("Command not supported or not in Programming Track mode."),
                (byte)'3' => new ResultError("Data provided is out of range."),
                (byte)'!' => new ResultOK(data[0]),
                _ => new ResultError("Unknown response from the NCE System.", data!)
            },
            _ => new ResultError("Unknown response from the NCE System.", data!)
        };
    }
}