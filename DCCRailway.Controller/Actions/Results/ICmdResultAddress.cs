using DCCRailway.Common.Types;

namespace DCCRailway.Controller.Actions.Results;

public interface ICmdResultAddress : ICmdResult {
    public DCCAddress Address { get; set; }
}