using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public DCCMomentum Momentum { get; set; }
}