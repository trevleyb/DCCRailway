using DCCRailway.Common.Types;

namespace DCCRailway.Station.Commands.Results;

public interface IResultPowerState {
    public DCCPowerState State { get; set; }
}