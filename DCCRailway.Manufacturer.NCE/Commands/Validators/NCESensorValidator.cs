using DCCRailway.DCCController.Commands.Results;
using DCCRailway.DCCController.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands.Validators;

/// <summary>
///     This is a special validation just for Sensors. It will either return 2 bytes for the sensors
///     or will return '0' to includate that the command is not supported
/// </summary>
public class NCESensorValidator : IResultValidation {
    public ICommandResult Validate(byte[]? data) => data?.Length switch {
        0 => CommandResult.Fail("Unexpected data returned and not processed. Expected 2 Bytes.", data!),
        1 => data[0] switch {
            (byte)'0' => CommandResult.Fail("Command not supported or not in Programming Track mode."),
            (byte)'3' => CommandResult.Fail("Data provided is out of range."),
            _         => CommandResult.Fail("Unknown response from the NCE Controller.", data!)
        },
        2 => CommandResult.Success(data),
        _ => CommandResult.Fail("Unknown response from the NCE Controller.", data!)
    } ?? CommandResult.Fail("Invalid data returned from the command execution.");
}