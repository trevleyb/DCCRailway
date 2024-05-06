using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types.Base;

public interface ILocoCmd {
    public IDCCAddress Address { get; set; }
}