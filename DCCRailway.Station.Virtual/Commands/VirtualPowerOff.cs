using DCCRailway.Common.Types;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Virtual.Adapters;

namespace DCCRailway.Station.Virtual.Commands;

[Command("PowerStateOff", "Set the Current State of the Power Supply to ON")]
public class VirtualPowerOff : VirtualCommand, ICmdPowerSetOff {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = new VirtualCommandResultPowerState(true);
        if (adapter is VirtualAdapter virtualAdapter) virtualAdapter.PowerState = DCCPowerState.Off;
        return result;
    }
    public DCCPowerState State { get; set; } = DCCPowerState.Unknown;
    public override string ToString() => "POWER STATE OFF";
}