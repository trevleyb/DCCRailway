using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validator;

namespace DCCRailway.System.NCE.Commands.Validators;

public class NCEProgrammingValidation : IResultValidation {
    public ICommandResult Validate(byte[]? data) {
        if (data is not { Length: 1 }) return CommandResult.Fail("Unexpected data returned and not processed.", data!);

        return data[0] switch {
            (byte)'0' => CommandResult.Fail("Programming Track is not enabled."),
            (byte)'3' => CommandResult.Fail("Short Circuit detected on the track."),
            (byte)'!' => CommandResult.Success(),
            _         => CommandResult.Fail("Unknown response from the NCE Controller.", data!)
        };
    }
}