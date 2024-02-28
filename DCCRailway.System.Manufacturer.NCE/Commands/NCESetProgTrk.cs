using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Manufacturer.NCE.Commands.Validators;

namespace DCCRailway.System.Manufacturer.NCE.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class NCESetProgTrk : NCECommand, ICmdTrackProg {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9E);

    public override string ToString() => "PROGRAMMING TRACK";
}