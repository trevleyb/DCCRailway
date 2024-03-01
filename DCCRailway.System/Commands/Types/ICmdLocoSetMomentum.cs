using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.BaseTypes;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public DCCMomentum Momentum { get; set; }
}