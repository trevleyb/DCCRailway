using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Systems.NCE.Commands.Validators;

namespace DCCRailway.Systems.NCE.Commands; 

public class NCEStopClock : NCECommand, ICmdClockStop, ICommand {
    public string Name => "NCE Stop Clock";

    public override IResult Execute(IAdapter adapter) {
        return SendAndReceieve(adapter, new NCEStandardValidation(), new byte[] {0x83});
    }

    public override string ToString() {
        return "STOP CLOCK";
    }
}