using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types.Base;

public interface ICmdAddress {
    public DCCAddress Address { get; set; }
}