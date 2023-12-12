using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Result;

public interface IResultPowerState {
    public DCCPowerState State { get; }
}