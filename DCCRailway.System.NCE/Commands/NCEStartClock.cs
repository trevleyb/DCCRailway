using DCCRailway.System.NCE.Commands.Validators;

namespace DCCRailway.System.NCE.Commands;

public class NCEStartClock : NCECommand, ICmdClockStart, ICommand {
    public string Name => "NCE Start Clock";

    public override IResult Execute(IAdapter adapter) {
        return SendAndReceieve(adapter, new NCEStandardValidation(), new byte[] { 0x84 });
    }

    public override string ToString() {
        return "START CLOCK ";
    }
}