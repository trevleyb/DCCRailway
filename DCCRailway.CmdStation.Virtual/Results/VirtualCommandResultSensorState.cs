using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Results;

public class VirtualCommandResultSensorState : CommandResult {
    public VirtualCommandResultSensorState(IDCCAddress address, bool state) : base(true, null, null) {
        Address = address;
        State   = state;
    }

    public IDCCAddress Address { get; init; }
    public bool        State   { get; init; }
}