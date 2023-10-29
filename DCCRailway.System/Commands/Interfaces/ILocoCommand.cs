using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces; 

public interface ILocoCommand {
    public IDCCAddress Address { get; }
}