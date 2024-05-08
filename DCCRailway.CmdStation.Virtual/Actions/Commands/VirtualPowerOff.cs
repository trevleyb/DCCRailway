using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Virtual.Actions.Results;
using DCCRailway.CmdStation.Virtual.Adapters;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("PowerStateOff", "Set the Current State of the Power Supply to ON")]
public class VirtualPowerOff : VirtualCommand, ICmdPowerSetOff {
    public override ICmdResult Execute(IAdapter adapter) {
        var result = new VirtualCmdResultPowerState(DCCPowerState.Off);
        if (adapter is VirtualAdapter virtualAdapter) virtualAdapter.PowerState = DCCPowerState.Off;
        return result;
    }
    public DCCPowerState State { get; set; } = DCCPowerState.Unknown;
    public override string ToString() => "POWER STATE OFF";
}