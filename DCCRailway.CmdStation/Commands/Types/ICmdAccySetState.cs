using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCmd {
    public DCCAccessoryState State { get; set; }
}