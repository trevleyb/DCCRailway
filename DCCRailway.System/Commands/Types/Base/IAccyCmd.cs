using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.Base;

public interface IAccyCmd {
    public IDCCAddress Address { get; set; }
}