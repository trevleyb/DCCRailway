using DCCRailway.Common.Types;

namespace DCCRailway.Station.Commands.Types.Base;

public interface ILocoCmd {
    public IDCCAddress Address { get; set; }
}