using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.BaseTypes;

public interface ICVCmd {
    public int          CV      { get; }
    public IDCCAddress? Address { get; set; }
}