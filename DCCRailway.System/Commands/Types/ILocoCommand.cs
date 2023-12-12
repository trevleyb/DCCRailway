using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType; 

public interface ILocoCommand {
    public IDCCAddress Address { get; set; }
}