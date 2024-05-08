using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands.Base;

public interface ICmdAddress {
    public DCCAddress Address { get; set; }
}