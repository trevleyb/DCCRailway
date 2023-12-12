using DCCRailway.System.Commands.Results;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE.Commands;

public class NCECommandResultSensorState : CommandResult {
      
    public NCECommandResultSensorState(IDCCAddress address, bool state) : base(true, null, null) {
        Address = address;
        State   = state;
    }
    public IDCCAddress Address { get; init; }
    public bool        State   { get; init; }
}