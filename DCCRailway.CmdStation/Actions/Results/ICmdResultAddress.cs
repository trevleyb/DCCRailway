using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Results;

public interface ICmdResultAddress : ICmdResult {
    public DCCAddress Address { get; set; }
}