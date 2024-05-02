using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Validators;
using DCCRailway.Station.NCE.Commands.Results;

namespace DCCRailway.Station.NCE.Commands;

[Command("ReadClock", "Read the Clock from the NCE Controller")]
public class NCEClockRead : Command, ICmdClockRead, ICommand {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), new byte[] { 0x82 });

        return result.IsOK ? result : new NCECommandResultClock(result.Data);
    }

    public override string ToString() => "READ CLOCK";
}