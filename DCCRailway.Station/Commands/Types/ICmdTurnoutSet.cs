using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdTurnoutSet : ICommand, IAccyCmd {
    public DCCTurnoutState State { get; set; }
}