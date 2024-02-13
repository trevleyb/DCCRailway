using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdConsistKill : ICommand {
    public IDCCAddress Address { get; set; }
}