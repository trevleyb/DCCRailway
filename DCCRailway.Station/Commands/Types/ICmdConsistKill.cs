using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdConsistKill : ICommand, IConsistCmd {
    public IDCCAddress Address { get; set; }
}