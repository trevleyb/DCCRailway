using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Results;

public class VirtualCmdResultSensorState : CmdResult {
    public VirtualCmdResultSensorState(DCCAddress address, bool state) : base(true, null, null) {
        Address = address;
        State   = state;
    }

    public DCCAddress Address { get; init; }
    public bool        State   { get; init; }
}