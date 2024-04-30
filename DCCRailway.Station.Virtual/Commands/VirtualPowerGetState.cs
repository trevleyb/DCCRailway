using DCCRailway.Common.Types;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("PowerGetState", "Get the Current State of the Power Supply")]
public class VirtualPowerGetState : VirtualCommand, ICmdPowerGetState {
    public override ICommandResult Execute(IAdapter adapter) {
        var result = new VirtualCommandResultPowerState(true) {
            State = DCCPowerState.Unknown
        };
        return result;
    }
    public DCCPowerState State { get; set; } = DCCPowerState.Unknown;
    public override string ToString() => "POWER STATE";
}