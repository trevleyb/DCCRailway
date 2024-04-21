using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdConsistDelete : ICommand, IConsistCmd {
    public IDCCAddress Address { get; set; }
}