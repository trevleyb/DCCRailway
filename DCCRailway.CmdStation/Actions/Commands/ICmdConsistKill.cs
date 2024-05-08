using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdConsistKill : ICommand, IConsistCmd {
    public DCCAddress Address { get; set; }
}