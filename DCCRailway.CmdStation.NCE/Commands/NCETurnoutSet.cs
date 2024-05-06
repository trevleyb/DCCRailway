using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Commands;

[Command("TurnoutSetState", "Set the state of a Turnout")]
public class NCETurnoutSet : NCEAccySetState, ICmdTurnoutSet, IAccyCmd {

    public override string ToString() => $"TURNOUT STATE ({Address} = {State})";
    public new DCCTurnoutState State {
        get => base.State == DCCAccessoryState.Closed  ? DCCTurnoutState.Closed : DCCTurnoutState.Thrown;
        set => base.State = value == DCCTurnoutState.Closed ? DCCAccessoryState.Closed : DCCAccessoryState.Thrown;
    }
}