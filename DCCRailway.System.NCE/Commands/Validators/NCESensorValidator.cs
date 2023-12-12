using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validator;

namespace DCCRailway.System.NCE.Commands.Validators;

/// <summary>
///     This is a special validation just for Sensors. It will either return 2 bytes for the sensors
///     or will return '0' to includate that the command is not supported
/// </summary>
public class NCESensorValidator : IResultValidation {
    public IResultOld Validate(byte[] data) =>
        data.Length switch {
            0 => new ResultOldError("Unexpected data returned and not processed. Expected 2 Bytes.", data!),
            1 => data[0] switch {
                (byte)'0' => new ResultOldError("Command not supported or not in Programming Track mode."),
                (byte)'3' => new ResultOldError("Data provided is out of range."),
                _         => new ResultOldError("Unknown response from the NCE System.", data!)
            },
            2 => new ResultOldOk(data),
            _ => new ResultOldError("Unknown response from the NCE System.", data!)
        };
}