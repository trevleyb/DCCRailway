using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;
using DCCRailway.Controller.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Validators;

public class NCEProgrammingValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) {
        if (data is not { Length: 1 }) return CmdResult.Fail(data!, "Unexpected data returned and not processed.");

        return data[0] switch {
            (byte)'0' => CmdResult.Fail("Programming Track is not enabled."),
            (byte)'3' => CmdResult.Fail("Short Circuit detected on the track."),
            (byte)'!' => CmdResult.Ok(),
            _         => CmdResult.Fail(data!, "Unknown response from the NCE CommandStation.")
        };
    }
}