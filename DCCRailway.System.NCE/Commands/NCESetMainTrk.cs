using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCESetMainTrk : NCECommand, ICmdTrackMain {
    public string Name => "NCE Switch to the Main Track";

    public override IResult Execute(IAdapter adapter) {
        return SendAndReceieve(adapter, new NCEStandardValidation(), 0x9F);
    }

    public override string ToString() {
        return "MAIN TRACK";
    }
}