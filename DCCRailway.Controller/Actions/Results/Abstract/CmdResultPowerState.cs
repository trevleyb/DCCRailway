using DCCRailway.Common.Types;

namespace DCCRailway.Controller.Actions.Results.Abstract;

public class CmdResultPowerState  : CmdResult, ICmdResultPowerState {
    public CmdResultPowerState(DCCPowerState state) : base() { State = state; }
    public CmdResultPowerState(bool success, DCCPowerState state) : base() { State = state; }
    public DCCPowerState State { get; init; }
}