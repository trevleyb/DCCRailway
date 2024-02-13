using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types; 

public interface ILocoCommand {
    public IDCCAddress Address { get; set; }
}