using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType; 

public interface IAccyCommand {
    public IDCCAddress Address { get; set; }
}