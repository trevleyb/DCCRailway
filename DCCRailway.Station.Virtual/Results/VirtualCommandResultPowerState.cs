using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Results;

namespace DCCRailway.Station.Virtual.Commands;

public class VirtualCommandResultPowerState : CommandResult, IResultPowerState {
    public VirtualCommandResultPowerState(bool isSuccess, CommandResultData? value = null, string? error = "") : base(isSuccess, value, error) { }
    public DCCPowerState State { get; set; }
}