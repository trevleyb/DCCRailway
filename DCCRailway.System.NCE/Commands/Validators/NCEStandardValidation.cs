using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validator;

namespace DCCRailway.System.NCE.Commands.Validators;

public class NCEStandardValidation : IResultValidation {
    public IResultOld Validate(byte[] data) {
        // Standard resultOld codes from the NCE system are as follows:
        // '0' = command not supported
        // '1' = loco address out of range
        // '2' = cab address out of range
        // '3' = data out of range
        // '4' = byte count out of range
        // '!' = command completed successfully
        if (data.Length != 1) return new ResultOldError("Unexpected data returned and not processed.", data!);

        return data[0] switch {
            (byte)'0' => new ResultOldError("Command not supported."),
            (byte)'1' => new ResultOldError("Loco address is out of range."),
            (byte)'2' => new ResultOldError("Cab address is out of range."),
            (byte)'3' => new ResultOldError("Data provided is out of range."),
            (byte)'4' => new ResultOldError("Byte count is out of range."),
            (byte)'!' => new ResultOldOk(data),
            _         => new ResultOldError("Unknown response from the NCE System.", data!)
        };
    }
}