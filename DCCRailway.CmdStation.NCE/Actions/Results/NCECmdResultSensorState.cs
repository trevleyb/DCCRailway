using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Actions.Results.Abstract;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Actions.Results;

public class NCECmdResultSensorState : CmdResult, ICmdResultAddress {
    public NCECmdResultSensorState(DCCAddress address, DCCAccessoryState state) : base(true, null, null) {
        Address = address;
        State   = state;
    }

    public DCCAddress Address { get; set; }
    public DCCAccessoryState State   { get; init; }
}