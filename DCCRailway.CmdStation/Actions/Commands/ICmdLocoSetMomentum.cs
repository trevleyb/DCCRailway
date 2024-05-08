using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public DCCMomentum Momentum { get; set; }
}