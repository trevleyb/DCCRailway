using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdConsistKill : ICommand, IConsistCmd {
    public DCCAddress Address { get; set; }
}