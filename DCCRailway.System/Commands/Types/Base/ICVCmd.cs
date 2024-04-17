using DCCRailway.Common.Types;

namespace DCCRailway.System.Commands.Types.Base;

public interface ICVCmd {
    public int          CV      { get; }
    public IDCCAddress? Address { get; set; }
}