using DCCRailway.Common.Types;

namespace DCCRailway.Controller.Actions.Results;

public interface ICmdResultPowerState : ICmdResult {
    DCCPowerState State { get; }
}