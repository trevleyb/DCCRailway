using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdConsistKill : ICommand, IConsistCmd {
    public DCCAddress Address { get; set; }
}