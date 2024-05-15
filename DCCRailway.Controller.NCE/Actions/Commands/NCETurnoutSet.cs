using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.NCE.Actions.Commands;

[Command("TurnoutSetState", "Set the state of a Turnout")]
public class NCETurnoutSet : NCEAccySetState, ICmdTurnoutSet, IAccyCmd {
    public override string ToString() => $"TURNOUT STATE ({Address} = {State})";

    public new DCCTurnoutState State {
        get => base.State == DCCAccessoryState.Closed ? DCCTurnoutState.Closed : DCCTurnoutState.Thrown;
        set => base.State = value == DCCTurnoutState.Closed ? DCCAccessoryState.Closed : DCCAccessoryState.Thrown;
    }
}