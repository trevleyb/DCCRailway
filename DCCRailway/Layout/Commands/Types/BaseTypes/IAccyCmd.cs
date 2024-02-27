using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types.BaseTypes;

public interface IAccyCmd {
    public IDCCAddress Address { get; set; }
}