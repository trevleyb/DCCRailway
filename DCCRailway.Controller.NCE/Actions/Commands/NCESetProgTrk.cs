using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Validators;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("SetProgTrk", "Switch Power to the Programming Track")]
public class NCESetProgTrk : NCECommand, ICmdTrackProg {
    protected override ICmdResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new NCEStandardValidation(), 0x9E);
    }

    public override string ToString() {
        return "PROGRAMMING TRACK";
    }
}