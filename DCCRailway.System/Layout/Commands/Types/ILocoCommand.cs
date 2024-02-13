using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types; 

public interface ILocoCommand {
    public IDCCAddress Address { get; set; }
}