using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Results.Abstract;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.NCE.Commands.Results;

public class NCECmdResultSensorState : CmdResult, ICmdResultAddress {
    public NCECmdResultSensorState(DCCAddress address, DCCAccessoryState state) : base(true, null, null) {
        Address = address;
        State   = state;
    }

    public DCCAddress Address { get; set; }
    public DCCAccessoryState State   { get; init; }
}