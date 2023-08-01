using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCESetProgTrk : NCECommand, ICmdTrackProg {
    public static string Name => "NCE Switch to the Programming Track";

    public override IResult Execute(IAdapter adapter) {
        return SendAndReceieve(adapter, new NCEStandardValidation(), 0x9E);
    }

    public override string ToString() {
        return "PROGRAMMING TRACK";
    }
}