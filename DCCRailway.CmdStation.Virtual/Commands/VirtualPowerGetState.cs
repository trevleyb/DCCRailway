using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Virtual.Adapters;
using DCCRailway.CmdStation.Virtual.Results;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("PowerGetState", "Get the Current State of the Power Supply")]
public class VirtualPowerGetState : VirtualCommand, ICmdPowerGetState {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = new VirtualCommandResultPowerState(true);
        if (adapter is VirtualAdapter virtualAdapter) result.State = virtualAdapter.PowerState;
        return result;
    }
    public DCCPowerState State { get; set; } = DCCPowerState.Unknown;
    public override string ToString() => "POWER STATE";
}