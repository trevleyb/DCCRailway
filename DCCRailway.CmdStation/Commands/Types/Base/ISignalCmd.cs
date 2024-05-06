using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types.Base;

public interface ISignalCmd {
    public IDCCAddress Address { get; set; }
}