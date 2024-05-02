using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;
using DCCRailway.Station.NCE.Commands.Validators;

namespace DCCRailway.Station.NCE.Commands;

[Command("TurnoutSetState", "Set the state of a Turnout")]
public class NCETurnoutSet : NCEAccySetState, ICmdTurnoutSet, IAccyCmd {

    public override string ToString() => $"TURNOUT STATE ({Address} = {State})";
    public new DCCTurnoutState State {
        get => base.State == DCCAccessoryState.Closed  ? DCCTurnoutState.Closed : DCCTurnoutState.Thrown;
        set => base.State = value == DCCTurnoutState.Closed ? DCCAccessoryState.Closed : DCCAccessoryState.Thrown;
    }
}