using DCCRailway.Common.Types;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Virtual.Adapters;

namespace DCCRailway.Station.Virtual.Commands;

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