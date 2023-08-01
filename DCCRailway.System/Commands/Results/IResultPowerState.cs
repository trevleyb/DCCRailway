using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Results;

public interface IResultPowerState {
    public DCCPowerState State { get; }
}