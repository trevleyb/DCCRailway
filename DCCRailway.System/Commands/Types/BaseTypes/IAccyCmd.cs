using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.BaseTypes;

public interface IAccyCmd {
    public IDCCAddress Address { get; set; }
}