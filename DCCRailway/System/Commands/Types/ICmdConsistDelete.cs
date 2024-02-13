using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdConsistDelete : ICommand {
    public IDCCAddress Address { get; set; }
}