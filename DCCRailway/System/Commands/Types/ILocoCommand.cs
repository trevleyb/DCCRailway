using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types; 

public interface ILocoCommand {
    public IDCCAddress Address { get; set; }
}