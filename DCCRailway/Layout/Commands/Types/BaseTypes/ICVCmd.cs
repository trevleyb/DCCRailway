using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types.BaseTypes;

public interface ICVCmd {
    public int          CV      { get; }
    public IDCCAddress? Address { get; set; }
}