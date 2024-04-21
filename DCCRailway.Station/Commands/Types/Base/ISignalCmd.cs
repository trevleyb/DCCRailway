using DCCRailway.Common.Types;

namespace DCCRailway.Station.Commands.Types.Base;

public interface ISignalCmd {
    public IDCCAddress Address { get; set; }
}