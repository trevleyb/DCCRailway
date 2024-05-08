using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Results;

public class VirtualCmdResultSensorState : CmdResult {
    public VirtualCmdResultSensorState(DCCAddress address, bool state) : base(true, null, null) {
        Address = address;
        State   = state;
    }

    public DCCAddress Address { get; init; }
    public bool        State   { get; init; }
}