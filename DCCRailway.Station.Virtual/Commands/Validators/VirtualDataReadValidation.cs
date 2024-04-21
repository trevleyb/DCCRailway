using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Validators;

namespace DCCRailway.Station.Virtual.Commands.Validators;

public class VirtualDataReadValidation : IResultValidation {
    public ICommandResult Validate(byte[]? data) => data?.Length switch {
        0 => CommandResult.Fail("Unexpected data returned and not processed. Expected 2 Bytes.", data!),
        1 => data[0] switch {
            (byte)'0' => CommandResult.Fail("Command not supported or not in Programming Track mode."),
            (byte)'3' => CommandResult.Fail("Data provided is out of range."),
            _         => CommandResult.Fail("Unknown response from the Virtual Controller.", data!)
        },
        2 => data[1] switch {
            (byte)'0' => CommandResult.Fail("Command not supported or not in Programming Track mode."),
            (byte)'3' => CommandResult.Fail("Data provided is out of range."),
            (byte)'!' => CommandResult.Success(data!),
            _         => CommandResult.Fail("Unknown response from the Virtual Controller.", data!)
        },
        _ => CommandResult.Fail("Unknown response from the Virtual Controller.", data!)
    } ?? CommandResult.Fail("No data returned from the command execution.");
}