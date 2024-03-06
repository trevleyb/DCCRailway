using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdConsistKill : ICommand, IConsistCmd {
    public IDCCAddress Address { get; set; }
}