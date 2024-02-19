using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Types;

namespace DCCRailway.Manufacturer.NCE.Commands.Results;

public class NCECommandResultSensorState : CommandResult {
      
    public NCECommandResultSensorState(IDCCAddress address, bool state) : base(true, null, null) {
        Address = address;
        State   = state;
    }
    public IDCCAddress Address { get; init; }
    public bool        State   { get; init; }
}