using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces; 

public interface IAccyCommand {
    public IDCCAddress Address { get; }
}