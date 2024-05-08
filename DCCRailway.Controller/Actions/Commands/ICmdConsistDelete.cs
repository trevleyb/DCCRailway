using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdConsistDelete : ICommand, IConsistCmd {
    public DCCAddress Address { get; set; }
}