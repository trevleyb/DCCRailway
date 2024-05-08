using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Actions.Commands;

[Command("TurnoutSetState", "Set the state of a Turnout")]
public class NCETurnoutSet : NCEAccySetState, ICmdTurnoutSet, IAccyCmd {

    public override string ToString() => $"TURNOUT STATE ({Address} = {State})";
    public new DCCTurnoutState State {
        get => base.State == DCCAccessoryState.Closed  ? DCCTurnoutState.Closed : DCCTurnoutState.Thrown;
        set => base.State = value == DCCTurnoutState.Closed ? DCCAccessoryState.Closed : DCCAccessoryState.Thrown;
    }
}