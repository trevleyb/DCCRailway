using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types.Base;

public interface ICVCmd {
    public int          CV      { get; }
    public IDCCAddress? Address { get; set; }
}