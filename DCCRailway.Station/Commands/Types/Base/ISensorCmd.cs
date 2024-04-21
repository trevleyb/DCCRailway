using DCCRailway.Common.Types;

namespace DCCRailway.Station.Commands.Types.Base;

public interface ISensorCmd {
    public IDCCAddress Address { get; set; }
}