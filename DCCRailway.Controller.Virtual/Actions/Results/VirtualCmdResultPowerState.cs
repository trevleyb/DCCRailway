using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;

namespace DCCRailway.Controller.Virtual.Actions.Results;

public class VirtualCmdResultPowerState(DCCPowerState state) : CmdResult, ICmdResultPowerState {
    public DCCPowerState State { get; set; } = state;
}