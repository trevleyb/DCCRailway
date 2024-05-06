using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Results;

public class VirtualCommandResultPowerState : CommandResult, IResultPowerState {
    public VirtualCommandResultPowerState(bool isSuccess, CommandResultData? value = null, string? error = "") : base(isSuccess, value, error) { }
    public DCCPowerState State { get; set; }
}