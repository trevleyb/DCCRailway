using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdConsistKill : ICommand {
    public IDCCAddress Address { get; set; }
}