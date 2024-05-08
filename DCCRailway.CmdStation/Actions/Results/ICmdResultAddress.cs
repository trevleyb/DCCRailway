using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Results;

public interface ICmdResultAddress : ICmdResult {
    public DCCAddress Address { get; set; }
}