using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types.Base;

public interface IAccyCmd {
    public IDCCAddress Address { get; set; }
}