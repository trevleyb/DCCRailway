using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdConsistDelete : ICommand {
    public IDCCAddress Address { get; set; }
}