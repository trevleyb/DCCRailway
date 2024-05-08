using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Results;

public interface ICmdResultPowerState : ICmdResult{
    DCCPowerState State { get; }
}