using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdConsistDelete : ICommand {
    public IDCCAddress Address { get; set; }
}