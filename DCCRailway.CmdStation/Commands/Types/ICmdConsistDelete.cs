using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdConsistDelete : ICommand, IConsistCmd {
    public IDCCAddress Address { get; set; }
}