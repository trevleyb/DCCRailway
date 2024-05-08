using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Results;

public class VirtualCmdResultPowerState(DCCPowerState state) : CmdResult, ICmdResultPowerState {
    public DCCPowerState State { get; set; } = state;
}