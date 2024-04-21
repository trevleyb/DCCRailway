using DCCRailway.Common.Types;

namespace DCCRailway.Station.Commands.Types.Base;

public interface IAccyCmd {
    public IDCCAddress Address { get; set; }
}