using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types; 

public interface IAccyCommand {
    public IDCCAddress Address { get; set; }
}