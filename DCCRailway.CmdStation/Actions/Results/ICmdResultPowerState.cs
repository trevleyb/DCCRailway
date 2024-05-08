using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Results;

public interface ICmdResultPowerState : ICmdResult{
    DCCPowerState State { get; }
}