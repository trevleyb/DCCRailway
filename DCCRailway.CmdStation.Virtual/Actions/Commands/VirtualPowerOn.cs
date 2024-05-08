using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Virtual.Adapters;
using DCCRailway.CmdStation.Virtual.Results;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("PowerStateOn", "Set the Current State of the Power Supply to ON")]
public class VirtualPowerOn : VirtualCommand, ICmdPowerSetOn {
    public override ICmdResult Execute(IAdapter adapter) {
        var result = new VirtualCmdResultPowerState(DCCPowerState.On);
        if (adapter is VirtualAdapter virtualAdapter) virtualAdapter.PowerState = DCCPowerState.On;
        return result;
    }
    public DCCPowerState State { get; set; } = DCCPowerState.Unknown;
    public override string ToString() => "POWER STATE ON";
}