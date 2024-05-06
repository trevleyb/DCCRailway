using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public DCCMomentum Momentum { get; set; }
}