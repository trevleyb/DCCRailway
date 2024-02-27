using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types.BaseTypes;

public interface ILocoCmd {
    public IDCCAddress Address { get; set; }
}