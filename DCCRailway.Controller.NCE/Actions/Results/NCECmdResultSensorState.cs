using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Results.Abstract;

namespace DCCRailway.Controller.NCE.Actions.Results;

public class NCECmdResultSensorState : CmdResult, ICmdResultAddress {
    public NCECmdResultSensorState(DCCAddress address, DCCAccessoryState state) : base(true, null, null) {
        Address = address;
        State   = state;
    }

    public DCCAccessoryState State { get; init; }

    public DCCAddress Address { get; set; }
}