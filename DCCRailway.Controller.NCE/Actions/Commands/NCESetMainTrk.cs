using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("SetMainTrk", "Switch Power to the MainLine")]
public class NCESetMainTrk : NCECommand, ICmdTrackMain {
    public override ICmdResult Execute(IAdapter adapter) => SendAndReceive(adapter, new NCEStandardValidation(), 0x9F);

    public override string ToString() => "MAIN TRACK";
}