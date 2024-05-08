using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Results;

public class VirtualCmdResultPowerState(DCCPowerState state) : CmdResult, ICmdResultPowerState {
    public DCCPowerState State { get; set; } = state;
}