using DCCRailway.Common.Types;

namespace DCCRailway.Controller.Actions.Results;

public interface ICmdResultAddress : ICmdResult {
    DCCAddress Address { get; set; }
}