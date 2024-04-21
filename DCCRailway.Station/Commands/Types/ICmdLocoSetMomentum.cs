using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public DCCMomentum Momentum { get; set; }
}