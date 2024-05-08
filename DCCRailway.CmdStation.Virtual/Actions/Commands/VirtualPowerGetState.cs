using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Virtual.Actions.Results;
using DCCRailway.CmdStation.Virtual.Adapters;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("PowerGetState", "Get the Current State of the Power Supply")]
public class VirtualPowerGetState : VirtualCommand, ICmdPowerGetState {
    public override ICmdResult Execute(IAdapter adapter) {
        var result = new VirtualCmdResultPowerState(DCCPowerState.On);
        if (adapter is VirtualAdapter virtualAdapter) result.State = virtualAdapter.PowerState;
        return result;
    }
    public DCCPowerState State { get; set; } = DCCPowerState.Unknown;
    public override string ToString() => "POWER STATE";
}