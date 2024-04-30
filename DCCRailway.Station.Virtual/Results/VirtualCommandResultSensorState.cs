using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Results;

namespace DCCRailway.Station.Virtual.Commands.Results;

public class VirtualCommandResultSensorState : CommandResult {
    public VirtualCommandResultSensorState(IDCCAddress address, bool state) : base(true, null, null) {
        Address = address;
        State   = state;
    }

    public IDCCAddress Address { get; init; }
    public bool        State   { get; init; }
}