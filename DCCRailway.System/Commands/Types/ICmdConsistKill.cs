using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdConsistKill : ICommand {
    public IDCCAddress Address { get; set; }
}