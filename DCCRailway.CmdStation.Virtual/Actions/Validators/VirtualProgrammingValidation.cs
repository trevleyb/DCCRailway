using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.CmdStation.Actions.Validators;

namespace DCCRailway.CmdStation.Virtual.Actions.Validators;

public class VirtualProgrammingValidation : IResultValidation {
    public ICmdResult Validate(byte[]? data) {
        if (data is not { Length: 1 }) return CmdResult.Fail(data!,"Unexpected data returned and not processed.");

        return data[0] switch {
            (byte)'0' => CmdResult.Fail("Programming Track is not enabled."),
            (byte)'3' => CmdResult.Fail("Short Circuit detected on the track."),
            (byte)'!' => CmdResult.Ok(),
            _         => CmdResult.Fail(data!,"Unknown response from the Virtual Controller.")
        };
    }
}