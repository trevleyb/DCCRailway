using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdAccySetState : ICommand, IAccyCmd {
    public DCCAccessoryState State { get; set; }
}