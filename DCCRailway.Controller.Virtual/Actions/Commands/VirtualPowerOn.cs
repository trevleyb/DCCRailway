using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Virtual.Actions.Results;
using DCCRailway.Controller.Virtual.Adapters;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("PowerStateOn", "Set the Current State of the Power Supply to ON")]
public class VirtualPowerOn : VirtualCommand, ICmdPowerSetOn {
    public DCCPowerState State { get; set; } = DCCPowerState.Unknown;

    protected override ICmdResult Execute(IAdapter adapter) {
        var result                                                              = new VirtualCmdResultPowerState(DCCPowerState.On);
        if (adapter is VirtualAdapter virtualAdapter) virtualAdapter.PowerState = DCCPowerState.On;
        return result;
    }

    public override string ToString() {
        return "POWER STATE ON";
    }
}