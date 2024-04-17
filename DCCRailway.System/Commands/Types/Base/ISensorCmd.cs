using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.Base;

public interface ISensorCmd {
    public IDCCAddress Address { get; set; }
}