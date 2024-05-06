using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Results;

public interface IResultPowerState {
    public DCCPowerState State { get; set; }
}