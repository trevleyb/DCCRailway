using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;

namespace DCCRailway.Manufacturer.NCE.Commands.Validators;

public class NCEStandardValidation : IResultValidation {
    public ICommandResult Validate(byte[]? data) {
        // Standard resultOld codes from the NCE controller are as follows:
        // '0' = command not supported
        // '1' = loco address out of range
        // '2' = cab address out of range
        // '3' = data out of range
        // '4' = byte count out of range
        // '!' = command completed successfully
        if (data != null && data.Length != 1) return CommandResult.Fail("Unexpected data returned and not processed.", data!);

        return data?[0] switch {
            (byte)'0' => CommandResult.Fail("Command not supported."),
            (byte)'1' => CommandResult.Fail("Loco address is out of range."),
            (byte)'2' => CommandResult.Fail("Cab address is out of range."),
            (byte)'3' => CommandResult.Fail("Data provided is out of range."),
            (byte)'4' => CommandResult.Fail("Byte count is out of range."),
            (byte)'!' => CommandResult.Success(data),
            _         => CommandResult.Fail("Unknown response from the NCE Controller.", data!)
        } ?? CommandResult.Fail("Invalid data returned from the command execution.");
    }
}