using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validator;

namespace DCCRailway.System.NCE.Commands.Validators;

public class NCEProgrammingValidation : IResultValidation {
    public IResultOld Validate(byte[] data) {
        if (data.Length != 1) return new ResultOldError("Unexpected data returned and not processed.", data!);

        return data[0] switch {
            (byte)'0' => new ResultOldError("Programming Track is not enabled."),
            (byte)'3' => new ResultOldError("Short Circuit detected on the track."),
            (byte)'!' => new ResultOldOk(),
            _         => new ResultOldError("Unknown response from the NCE System.", data!)
        };
    }
}